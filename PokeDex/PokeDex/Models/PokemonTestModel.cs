using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PokeApiNet;
using Xamarin.Forms;

namespace PokeDex.Models
{
    class PokemonTestModel
    {
        PokeApiClient pokeApiClient;
        HttpClient client;

        public string _name;
        public int _id;
        public string _type1;
        public string _type2;
        public ImageSource _imageSource;

        public PokemonTestModel()
        {
            pokeApiClient = new PokeApiClient();
            client = new HttpClient();
        }

        public async Task GetEmAll(string pokemon)
        {
            this._imageSource = await FindPokemonImageAsync(pokemon);
            await FindPokemonAsync(pokemon);
        }

        public async Task<ImageSource> FindPokemonImageAsync(string pokemonID_Name)
        {
            Pokemon pokemon = await pokeApiClient.GetResourceAsync<Pokemon>(pokemonID_Name);
            Stream stream = await client.GetStreamAsync(pokemon.Sprites.FrontDefault);
            return ImageSource.FromStream(() => stream);
        }

        public async Task<string> FindPokemonNameAsync(string pokemonID_Name)
        {
            Pokemon pokemon = await pokeApiClient.GetResourceAsync<Pokemon>(pokemonID_Name);
            string name = pokemon.Name;

            return name;
        }

        public async Task FindPokemonAsync(string pokemonID_Name)
        {
            Pokemon pokemon = await pokeApiClient.GetResourceAsync<Pokemon>(pokemonID_Name);
            string name = pokemon.Name;
            int id = pokemon.Id;
            
            List<String> type = new List<string>();

            for (int i = 0; i < pokemon.Types.Count; i++)
            {
                type.Add(pokemon.Types[i].Type.Name);
            }

            List<Tuple<string, string>> stats = new List<Tuple<string, string>>();

            for (int i = 0; i < pokemon.Stats.Count; i++)
            {
                stats.Add(Tuple.Create(pokemon.Stats[i].Stat.Name, pokemon.Stats[i].BaseStat.ToString()));
            }

            this._name = name;
            this._id = id;
            this._type1 = type[0]?? "none";
            this._type2 = type[1] ?? "none";
        }
    }
}
