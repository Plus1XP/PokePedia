using System;
using System.Collections.Generic;
using System.Text;

namespace PokeDex.Models
{
    public class PokedexModel
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public List<string> Types { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public List<string> Abilities { get; set; }
        public int BaseXP { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefence { get; set; }
        public int Speed { get; set; }
        public string FootprintsImageSource { get; set; }
        public string RemasteredThumbImageSource { get; set; }
        public string ModernThumbImageSource { get; set; }
        public string GreenArtImageSource { get; set; }
        public string BlueArtImageSource { get; set; }
        public string ModernArtImageSource { get; set; }

        public Species species { get; set; } = new Species();
    }

    public class Species
    {
        public int CaptureRate { get; set; }
        public string Colour { get; set; }
        public List<string> EggGroups { get; set; }
        public string Bio1 { get; set; }
        public string Bio2 { get; set; }
        public string Genus { get; set; }
        public string Generation { get; set; }
        public string GrowthRate { get; set; }
        public string Habitat { get; set; }
        public string Shape { get; set; }
    }
}
