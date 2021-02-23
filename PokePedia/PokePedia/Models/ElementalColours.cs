using System;
using System.Collections.Generic;

namespace PokePedia.Models
{
    public class ElementalColours
    {
        public Dictionary<string, string> typeColour;

        public ElementalColours()
        {
            typeColour = new Dictionary<string, string>
            {
                { "normal", "#A8A878" },
                { "fire", "#F08030" },
                { "water", "#6890F0" },
                { "grass", "#78C850" },
                { "electric", "#F8D030" },
                { "ice", "#98D8D8" },
                { "fighting", "#C03028" },
                { "poison", "#A040A0" },
                { "ground", "#E0C068" },
                { "flying", "#96C3EB" }, // Custom
                { "psychic", "#F85888" },
                { "bug", "#A8B820" },
                { "rock", "#B8A038" },
                { "ghost", "#705898" },
                { "dark", "#CCaC93" }, // Custom
                { "dragon", "#7038F8" },
                { "steel", "#B8B8B8" }, // Custom
                { "fairy", "#EE99AC" }
            };
        }

        public Tuple<string, string> GetElementalColour(List<string> types)
        {
            if (types.Count == 1)
            {
                return Tuple.Create(typeColour[types[0].ToLower()], typeColour[types[0].ToLower()]);
            }
            else
            {
                return Tuple.Create(typeColour[types[0].ToLower()], typeColour[types[1].ToLower()]);
            }
        }

        public Tuple<string, string> GetElementalBackgroundColour(List<string> types)
        {
            if (types.Count == 1)
            {
                return Tuple.Create(typeColour[types[0].ToLower()], "#FFFFFF"); // Return White if no second type available
            }
            else
            {
                return Tuple.Create(typeColour[types[0].ToLower()], typeColour[types[1].ToLower()]);
            }
        }
    }
}