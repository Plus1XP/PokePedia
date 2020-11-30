using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokeDex
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            TestAPI("4");
        }

        public async void TestAPI(string poke)
        {
            Models.PokemonModel model = new Models.PokemonModel();
            IMG_HERE.Source = await model.FindPokemonImageAsync(poke);
            await model.FindPokemonAsync(poke);
            DSRPT_HERE.Text = model._name;
            
        }
    }
}
