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

        private DataManager dataManager;

        private int pkmToFind = 151;

        public MainPageViewModel()
        {
            detailsPageView = new DetailsPage();
            detailsPageViewModel = new DetailsPageViewModel();
            pkmManager = new PokedexManagerModel();

            dataManager = new DataManager();

            OnSearch = new AsyncRelayCommand(() => OnLoadPokemonList(pkmToFind));

            IsTapped = new Command<PokedexModel>(async p => await ItemTapped(p));

            OnClearData = new Command(ClearData);

            OnSearch.Execute(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AsyncRelayCommand OnSearch { get; private set; }

        public Command<PokedexModel> IsTapped { get; private set; }

        public Command OnClearData { get; private set; }

        public ObservableCollection<PokedexModel> pkmList { get; private set; }

        public string Search_Header { get; private set; } = "Search";

        public string Logo { get; private set; } = $"Images/Original/Logo.png";

        private void OnPropertChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public async Task OnLoadPokemonList(int pkmToFind)
        {
            pkmList = new ObservableCollection<PokedexModel>(await dataManager.LoadPokemonDataList(pkmToFind));
            OnPropertChanged(null);
        }

        private async Task ItemTapped(PokedexModel pkm)
        {
            MessagingCenter.Send<MainPageViewModel, PokedexModel>(this, "Send_Selected_Pokemon", pkm);
            detailsPageView.BindingContext = detailsPageViewModel;
            await Application.Current.MainPage.Navigation.PushAsync(detailsPageView);
        }

        private void ClearData()
        {
            dataManager.RemovePokemonDataFile();
            OnSearch.Execute(null);
        }
    }
}
