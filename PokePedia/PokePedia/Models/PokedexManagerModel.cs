using PokeApiNet;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokePedia.Models
{
    public class PokedexManagerModel
    {
        PokeApiClient pokeApi;
        PokedexModel pkm;

        HttpClient client;

        public PokedexManagerModel()
        {
            pokeApi = new PokeApiClient();
            client = new HttpClient();
        }

        public async Task PopulatePokemonList(ObservableCollection<PokedexModel> pkmList, int maxPkm)
        {
            pkmList.Clear();

            for (int i = 1; i <= maxPkm; i++)
            {

                pkmList.Add(await CreatePkm($"{i}"));
            }
        }

        public async Task<List<PokedexModel>> GetPokemonList(int maxPkm)
        {
            List<PokedexModel> pkmList = new List<PokedexModel>();

            for (int i = 1; i <= maxPkm; i++)
            {
                pkmList.Add(await CreatePkm($"{i}"));
            }

            return pkmList;
        }

        public async Task UpdatePokemonSpeciesList(ObservableCollection<PokedexModel> pkmList)
        {
            foreach (PokedexModel pkm in pkmList)
            {
                await GetSpeciesInfo(pkm, pkm.ID.ToString());
            }
        }

        public async Task<ObservableCollection<PokedexModel>> GetPokemonSpeciesList(ObservableCollection<PokedexModel> pkmList)
        {
            foreach (PokedexModel pkm in pkmList)
            {
                await GetSpeciesInfo(pkm, pkm.ID.ToString());
            }

            return pkmList;
        }

        //TEST

        public async Task PopulatePokemonListWithSpecies(ObservableCollection<PokedexModel> pkmList, int maxPkm)
        {
            pkmList.Clear();

            for (int i = 1; i <= maxPkm; i++)
            {
                pkmList.Add(await GetSinglePokemonSpecies(await CreatePkm($"{i}")));
            }
        }


        public async Task<PokedexModel> GetSinglePokemonSpecies(PokedexModel pkm)
        {
            return await GetSpeciesInfo(pkm, pkm.ID.ToString());
        }

        //TEST

        public async Task<PokedexModel> CreatePkm(string pkmID)
        {
            try
            {
                pkm = new PokedexModel();

                Pokemon pokemon = await pokeApi.GetResourceAsync<Pokemon>(pkmID);

                pkm.Name = pokemon.Name;
                pkm.ID = pokemon.Id;
                pkm.Types = GetTypes(pkm.Types, pokemon);
                pkm.Weight = pokemon.Weight;
                pkm.Height = pokemon.Height;
                pkm.Abilities = GetAbilities(pkm.Abilities, pokemon);
                pkm.BaseXP = pokemon.BaseExperience;
                pkm.HP = pokemon.Stats[0].BaseStat;
                pkm.Attack = pokemon.Stats[1].BaseStat;
                pkm.Defence = pokemon.Stats[2].BaseStat;
                pkm.SpecialAttack = pokemon.Stats[3].BaseStat;
                pkm.SpecialDefence = pokemon.Stats[4].BaseStat;
                pkm.Speed = pokemon.Stats[5].BaseStat;
                //pkm.LowResImageSource = pokemon.Sprites.FrontDefault;
                pkm.RemasteredThumbImageSource = $"Data/RemasteredThumbs/{pokemon.Id}.png";
                pkm.ModernThumbImageSource = $"Data/ModernThumbs/{pokemon.Id}.png";
                pkm.FootprintsImageSource = $"Data/Footprints/{pokemon.Id}.png"; ;
                pkm.GreenArtImageSource = $"Data/GreenArt/{pokemon.Id}.png";
                pkm.BlueArtImageSource = $"Data/BlueArt/{pokemon.Id}.png";
                pkm.ModernArtImageSource = $"Data/ModernArt/{pokemon.Id}.png";
                await GetSpeciesInfo(pkm, pkmID);
            }
            catch (Exception ex)
            {

                throw new Exception($"!! ERROR !! \n{ex.Message} \n{ex.InnerException} \n{ex.StackTrace} \n{ex.Source}");
            }

            return pkm;
        }

        public async Task<PokedexModel> GetSpeciesInfo(PokedexModel pkm, string pkmID)
        {
            try
            {
                PokemonSpecies species = await pokeApi.GetResourceAsync<PokemonSpecies>(pkmID);
                //pkm.species = new Species();
                //pkm.species.Bio = Regex.Replace(species.FlavorTextEntries[0].Language , @"\n+", " ");
                pkm.species.Bio1 = GetFlavourText(species, "en", "red");
                pkm.species.Bio2 = GetFlavourText(species, "en", "gold");
                pkm.species.CaptureRate = species.CaptureRate;
                pkm.species.Colour = species.Color.Name;
                pkm.species.EggGroups = GetEggGroups(pkm.species.EggGroups, species);
                pkm.species.Generation = species.Generation.Name;
                pkm.species.Genus = species.Genera[7].Genus;
                pkm.species.GrowthRate = species.GrowthRate.Name;
                pkm.species.Habitat = species.Habitat.Name;
                pkm.species.Shape = species.Shape.Name;
            }
            catch (Exception ex)
            {

                throw new Exception($"!! ERROR !! \n{ex.Message} \n{ex.InnerException} \n{ex.StackTrace} \n{ex.Source}");
            }

            return pkm;
        }

        public List<string> GetAbilities(List<string> list, Pokemon pokemon)
        {
            list = new List<string>();

            for (int i = 0; i < pokemon.Abilities.Count; i++)
            {
                list.Add(pokemon.Abilities[i].Ability.Name);
            }

            return list;
        }

        public List<string> GetTypes(List<string> list, Pokemon pokemon)
        {
            list = new List<string>();

            for (int i = 0; i < pokemon.Types.Count; i++)
            {
                list.Add(pokemon.Types[i].Type.Name);
            }

            return list;
        }

        public List<string> GetEggGroups(List<string> list, PokemonSpecies species)
        {
            list = new List<string>();

            for (int i = 0; i < species.EggGroups.Count; i++)
            {
                list.Add(species.EggGroups[i].Name);
            }

            return list;
        }

        public string GetFlavourText(PokemonSpecies species, string Language, string Version)
        {
            string bio = string.Empty;

            foreach (PokemonSpeciesFlavorTexts item in species.FlavorTextEntries)
            {
                if (item.Language.Name.Equals(Language) && item.Version.Name.Equals(Version))
                {
                    return Regex.Replace(item.FlavorText, @"\n", " ").Replace("\f", " ").Replace("\u000c", " ").Replace("POKéMON", "Pokémon");
                }
            }

            return bio;
        }

        public string GetHighResImage(int pokeID)
        {
            return "https://pokeres.bastionbot.org/images/pokemon/" + pokeID + ".png";
        }

        public int SetNameWidth(ObservableCollection<PokedexModel> pkmList)
        {
            //pkmList.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
            return pkmList.OrderByDescending(s => s.Name.Length).First().Name.Length;
        }
    }
}
