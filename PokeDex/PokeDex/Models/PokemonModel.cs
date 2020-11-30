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
    class PokemonModel
    {
        PokeApiClient pokeApiClient;
        HttpClient client;

        public string _name;

        public PokemonModel()
        {
            pokeApiClient = new PokeApiClient();
            client = new HttpClient();
        }

        public async Task<ImageSource> FindPokemonImageAsync(string pokemonID_Name)
        {
            Pokemon pokemon = await pokeApiClient.GetResourceAsync<Pokemon>(pokemonID_Name);
            Stream stream = await client.GetStreamAsync(pokemon.Sprites.FrontDefault);
            return ImageSource.FromStream(() => stream);
        }

        public async Task FindPokemonAsync(string pokemonID_Name)
        {
            Pokemon pokemon = await pokeApiClient.GetResourceAsync<Pokemon>(pokemonID_Name);
            string name = pokemon.Name;
            string id = pokemon.Id.ToString();
            
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
        }
    }
}
