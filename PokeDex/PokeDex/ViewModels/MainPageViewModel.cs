using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PokeDex.Models;
using PokeDex.Views;

using Xamarin.Forms;

namespace PokeDex.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private PokedexManagerModel pkmManager;
        private DetailsPage detailsPageView;
        private DetailsPageViewModel detailsPageViewModel;

        public MainPageViewModel()
        {
            detailsPageView = new DetailsPage();
            detailsPageViewModel = new DetailsPageViewModel();
            pkmManager = new PokedexManagerModel();

            pkmList = new ObservableCollection<PokedexModel>();

            OnSearch = new AsyncRelayCommand(() => pkmManager.PopulatePokemonList(pkmList, 25));
            IsTapped = new Command<PokedexModel>(async p => await ItemTapped(p));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AsyncRelayCommand OnSearch { get; private set; }

        public Command<PokedexModel> IsTapped { get; private set; }

        public ObservableCollection<PokedexModel> pkmList { get; private set; }

        public string Header { get; private set; } = "Search";

        private void OnPropertChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private async Task ItemTapped(PokedexModel pkm)
        {
            MessagingCenter.Send<MainPageViewModel, PokedexModel>(this, "Send_Selected_Pokemon", pkm);
            detailsPageView.BindingContext = detailsPageViewModel;
            await Application.Current.MainPage.Navigation.PushAsync(detailsPageView);
        }
    }
}
