using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokeApiNet;

namespace PokeDex.Models
{
    public class PokedexManagerModel
    {
        PokeApiClient pokeApi;
        PokedexModel pkm;

        public PokedexManagerModel()
        {
            pokeApi = new PokeApiClient();
        }

        public async Task PopulatePokemonList(ObservableCollection<PokedexModel> pkmList, int maxPkm)
        {
            pkmList.Clear();

            for (int i = 1; i <= maxPkm; i++)
            {

                pkmList.Add(await CreatePkm($"{i}"));
            }
        }

        public async Task<List<PokedexModel>> PopulatePokemonList(int maxPkm)
        {
            List<PokedexModel> pkmList = new List<PokedexModel>();

            for (int i = 1; i <= maxPkm; i++)
            {
                pkmList.Add(await CreatePkm($"{i}"));
            }

            return pkmList;
        }

        public async Task<PokedexModel> CreatePkm(string pkmID)
        {
            try
            {
                pkm = new PokedexModel();

                Pokemon pokemon = await pokeApi.GetResourceAsync<Pokemon>(pkmID);

                pkm.Name = pokemon.Name;
                pkm.ID = pokemon.Id;
                pkm.Types = GetTypes(pkm.Types, pokemon);
                //pkm.Type1 = pokemon.Types[0].Type.Name;
                //pkm.Type2 = pokemon.Types[1].Type.Name ?? "";
                pkm.Weight = pokemon.Weight;
                pkm.Height = pokemon.Height;
                pkm.Abilities = GetAbilities(pkm.Abilities, pokemon);
                //pkm.Ability1 = pokemon.Abilities[0].Ability.Name;
                //pkm.Ability2 = pokemon.Abilities[1].Ability.Name ?? "";
                pkm.BaseXP = pokemon.BaseExperience;
                pkm.HP = pokemon.Stats[0].BaseStat;
                pkm.Attack = pokemon.Stats[1].BaseStat;
                pkm.Defence = pokemon.Stats[2].BaseStat;
                pkm.SpecialAttack = pokemon.Stats[3].BaseStat;
                pkm.SpecialDefence = pokemon.Stats[4].BaseStat;
                pkm.Speed = pokemon.Stats[5].BaseStat;
                pkm.Bio = "Blank!";
                pkm.imageSource = pokemon.Sprites.FrontDefault;
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

        public int SetNameWidth(ObservableCollection<PokedexModel> pkmList)
        {
            //pkmList.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
            return pkmList.OrderByDescending(s => s.Name.Length).First().Name.Length;
        }
    }
}
