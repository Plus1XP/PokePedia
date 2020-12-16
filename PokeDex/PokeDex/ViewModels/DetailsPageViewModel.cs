using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using Microcharts;

using SkiaSharp;

using PokeDex.Models;

using Xamarin.Forms;
using PokeDex.Views;

namespace PokeDex.ViewModels
{
    class DetailsPageViewModel : INotifyPropertyChanged
    {
        PokemonCharts chart;
        ElementalColours pkmColour;

        public DetailsPageViewModel()       // Add Height & Weight
        {
            pkmColour = new ElementalColours();
            MessagingCenter.Subscribe<MainPageViewModel, PokedexModel>(this, "Send_Selected_Pokemon", (sender, args) => { UpdatePokemonDetails(args); });
            CloseDetail = new Command(async () => await OnDetailsClosed());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Command CloseDetail { get; set; }

        public string TypeColour1 { get; private set; }

        public string TypeColour2 { get; private set; }

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

            SetTypeColour();

            CreatePokemonCharts();

            OnPropertChanged(null);

            MessagingCenter.Unsubscribe<MainPageViewModel>(this, "Send_Selected_Pokemon");
        }

        private void CreatePokemonCharts()
        {
            chart = new PokemonCharts();

            chart.chartEntries = new List<Microcharts.ChartEntry>
            {
                chart.AddChartEntries("HP", HP, pkmColour.GetElementalColour(Types).Item1),
                chart.AddChartEntries("Attack", Attack, pkmColour.GetElementalColour(Types).Item1),
                chart.AddChartEntries("Defence", Defence, pkmColour.GetElementalColour(Types).Item1),
                chart.AddChartEntries("S/Attack", SpecialAttack, pkmColour.GetElementalColour(Types).Item2),
                chart.AddChartEntries("S/Defence", SpecialDefence, pkmColour.GetElementalColour(Types).Item2),
                chart.AddChartEntries("Speed", Speed, pkmColour.GetElementalColour(Types).Item2)
            };

            StatsChart = new RadarChart { Entries = chart.GetChartEntries(), LabelTextSize = 30 };
        }

        private void SetTypeColour()
        {
            TypeColour1 = pkmColour.GetElementalBackgroundColour(Types).Item1;
            TypeColour2 = pkmColour.GetElementalBackgroundColour(Types).Item2;
        }

        private async Task OnDetailsClosed()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task OnSwipeRight()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetailsPage());
            //MessagingCenter.Send<MainPageViewModel, PokedexModel>(this, "Send_Selected_Pokemon", pkm);
        }

        private async Task OnSwipeLeft()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetailsPage());
            //MessagingCenter.Send<MainPageViewModel, PokedexModel>(this, "Send_Selected_Pokemon", pkm);
        }
    }
}