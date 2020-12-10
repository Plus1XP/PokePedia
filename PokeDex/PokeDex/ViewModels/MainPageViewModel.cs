using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using PokeDex.Models;
using PokeDex.Views;

using Xamarin.Forms;

namespace PokeDex.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private PokedexManagerModel pkmManager;

        private PokedexModel selectedPokemon;

        public MainPageViewModel()
        {
            pkmManager = new PokedexManagerModel();
            pkmList = new ObservableCollection<PokedexModel>();

            OnSearch = new AsyncRelayCommand(() => pkmManager.PopulatePokemonList(pkmList, 3));
            //IsTapped = new AsyncRelayCommand(ItemTapped);
            IsTapped = new Command<PokedexModel>(async p => await ItemTapped(p));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AsyncRelayCommand OnSearch { get; private set; }
        public Command<PokedexModel> IsTapped { get; private set; }

        public ObservableCollection<PokedexModel> pkmList { get; private set; }

        public string Header { get; private set; } = "Search";

        public PokedexModel SelectedPokemon
        {
            get => selectedPokemon;
            set
            {
                selectedPokemon = value;
                OnPropertChanged("SelectedPokemon");
            }
        }   

        private void OnPropertChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private async Task ItemTapped(PokedexModel pkm)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetailsPage());
            MessagingCenter.Send<MainPageViewModel, PokedexModel>(this,"Pokemon Details",pkm);
        }
    }
}
