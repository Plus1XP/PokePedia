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

        public DetailsPageViewModel()
        {
            pkmColour = new ElementalColours();
            MessagingCenter.Subscribe<MainPageViewModel, PokedexModel>(this, "Send_Selected_Pokemon", (sender, args) => { UpdatePokemonDetails(args); });
            CloseDetail = new Command(async () => await OnDetailsClosed());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Command CloseDetail { get; set; }

        public string TypeColour1_BackgroundColour { get; private set; }

        public string TypeColour2_BackgroundColour { get; private set; }

        public string Type_Header { get; private set; }

        public string Ability_Header { get; private set; }

        public string Height_Header { get; private set; }

        public string Weight_Header { get; private set; }

        public string Shape_Header { get; private set; }

        public string Habitat_Header { get; private set; }

        public string CaptureR_Header { get; private set; }

        public string GrowthR_Header { get; private set; }

        public string Name { get; private set; }

        public int? ID { get; private set; }

        public List<string> Types { get; private set; }

        public string Weight { get; private set; }

        public string Height { get; private set; }

        public List<string> Abilities { get; private set; }
        public int? BaseXP { get; private set; }

        public int? HP { get; private set; }

        public int? Attack { get; private set; }

        public int? Defence { get; private set; }

        public int? SpecialAttack { get; private set; }

        public int? SpecialDefence { get; private set; }

        public int? Speed { get; private set; } = null;

        public string Genus { get; private set; }

        public string Bio { get; private set; }

        public List<string> EggGroups { get; private set; }

        public string Shape { get; private set; }

        public string Colour { get; private set; }

        public string Habitat { get; private set; }

        public string GrowthRate { get; private set; }

        public int? CaptureRate { get; private set; }

        public string Generation { get; private set; }

        public string EggGroup { get; private set; }

        public string HighImage { get; private set; }

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
            SetHeaders();

            SetPokemonValues(pkm);

            SetTypeColour();

            CreatePokemonCharts();

            OnPropertChanged(null);

            MessagingCenter.Unsubscribe<MainPageViewModel>(this, "Send_Selected_Pokemon");
        }

        private void SetHeaders()
        {
            Type_Header = "TYPE";
            Ability_Header = "ABILITY";
            Height_Header = "Height";
            Weight_Header = "Weight";
            Shape_Header = "Shape";
            Habitat_Header = "Habitat";
            CaptureR_Header = "Capture Rate";
            GrowthR_Header = "Growth Rate";
        }

        private void SetPokemonValues(PokedexModel pkm)
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

            EggGroup = $"*This Pokémon belongs to the following Egg Group:\n{GetEggGroups(EggGroups).Item1} {GetEggGroups(EggGroups).Item2}";
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
            TypeColour1_BackgroundColour = pkmColour.GetElementalBackgroundColour(Types).Item1;
            TypeColour2_BackgroundColour = pkmColour.GetElementalBackgroundColour(Types).Item2;
        }

        private Tuple<string, string> GetEggGroups(List<string> eggGroup)
        {
            if (eggGroup.Count == 1)
            {
                return Tuple.Create(eggGroup[0], "");
            }
            else
            {
                return Tuple.Create(eggGroup[0], eggGroup[1]);
            }
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