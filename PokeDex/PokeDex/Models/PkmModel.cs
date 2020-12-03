using System;
using System.Collections.Generic;
using System.Text;

namespace PokeDex.Models
{
    public class PkmModel
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Ability1 { get; set; }
        public string Ability2 { get; set; }
        public int BaseXP { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefence { get; set; }
        public int Speed { get; set; }
        public string Bio { get; set; }

        public List<string> Types { get; set; }
        public List<string> Abilities { get; set; }

        public PkmModel()
        {

        }

    }
}
