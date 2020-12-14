using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using Microcharts;

using SkiaSharp;

using PokeDex.Models;

using Xamarin.Forms;

namespace PokeDex.ViewModels
{
    class DetailsPageViewModel : INotifyPropertyChanged
    {
        private readonly string Red = "#FF0000";
        private readonly string Green = "#008000";

        public DetailsPageViewModel()
        {
            MessagingCenter.Subscribe<MainPageViewModel, PokedexModel>(this, "Send_Selected_Pokemon", (sender, args) => { UpdatePokemonDetails(args); });
            CloseDetail = new Command(async () => await OnDetailsClosed());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Command CloseDetail { get; set; }

        public string Name { get; private set; } = "";

        public int? ID { get; private set; } = null;

        public List<string> Types { get; private set; } = null;

        public string Weight { get; private set; } = "";


        public string Height { get; private set; } = "";

        public List<string> Abilities { get; private set; } = null;
        public int? BaseXP { get; private set; } = null;

        public int? HP { get; private set; } = null;

        public int? Attack { get; private set; } = null;

        public int? Defence { get; private set; } = null;

        public int? SpecialAttack { get; private set; } = null;

        public int? SpecialDefence { get; private set; } = null;

        public int? Speed { get; private set; } = null;

        public string Genus { get; private set; } = "";

        public string Bio { get; private set; } = "";

        public List<string> EggGroups { get; private set; } = null;

        public string Shape { get; private set; } = "";

        public string Colour { get; private set; } = "";

        public string Habitat { get; private set; } = "";

        public string GrowthRate { get; private set; } = "";

        public int? CaptureRate { get; private set; } = null;

        public string Generation { get; private set; } = "";

        public string HighImage { get; private set; } = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/132.png";

        public Chart StatsChart { get; set; }

        public List<Microcharts.ChartEntry> stats => new List<Microcharts.ChartEntry>
        {
            new Microcharts.ChartEntry((float)HP)
            {
                Label = "HP",
                ValueLabel = HP.ToString(),
                Color=SKColor.Parse(Green)
            },
            new Microcharts.ChartEntry((float)Attack)
            {
                Label = "Attack",
                ValueLabel = Attack.ToString(),
                Color=SKColor.Parse(Green)
            },
            new Microcharts.ChartEntry((float)Defence)
            {
                Label = "Defense",
                ValueLabel = Defence.ToString(),
                Color=SKColor.Parse(Green)
            },
            new Microcharts.ChartEntry((float)SpecialAttack)
            {
                Label = "S/Attack",
                ValueLabel = SpecialAttack.ToString(),
                Color=SKColor.Parse(Red)
            },
            new Microcharts.ChartEntry((float)SpecialDefence)
            {
                Label = "S/Defence",
                ValueLabel = SpecialDefence.ToString(),
                Color=SKColor.Parse(Red)
            },
            new Microcharts.ChartEntry((float)Speed)
            {
                Label = "Speed",
                ValueLabel = Speed.ToString(),
                Color=SKColor.Parse(Red)
            },
        };

        private void OnPropertChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void UpdatePokemonDetails(PokedexModel pkm)
        {
            Name = pkm.Name;
            ID = pkm.ID;
            Types = pkm.Types;
            Weight = pkm.Weight.ToString();
            Height = pkm.Height.ToString();
            Abilities = pkm.Abilities;
            BaseXP = pkm.BaseXP;
            HP = pkm.HP;
            Attack = pkm.Attack;
            Defence = pkm.Defence;
            SpecialAttack = pkm.SpecialAttack;
            SpecialDefence = pkm.SpecialDefence;
            Speed = pkm.Speed;
            Genus = pkm.species.Genus;
            Bio = pkm.species.Bio;
            EggGroups = pkm.species.EggGroups;
            Shape = pkm.species.Shape;
            Colour = pkm.species.Colour;
            Habitat = pkm.species.Habitat;
            GrowthRate = pkm.species.GrowthRate;
            CaptureRate = pkm.species.CaptureRate;
            Generation = pkm.species.Generation;
            HighImage = pkm.HighResImageSource;

            StatsChart = new RadarChart { Entries = stats, LabelTextSize=30 };

            OnPropertChanged(null);

            MessagingCenter.Unsubscribe<MainPageViewModel>(this, "Send_Selected_Pokemon");
        }

        private async Task OnDetailsClosed()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
