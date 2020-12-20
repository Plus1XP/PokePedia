using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
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

        private ObservableCollection<PokedexModel> searchResults;

        private int pkmToFind = 151;

        public MainPageViewModel()
        {
            detailsPageView = new DetailsPage();
            detailsPageViewModel = new DetailsPageViewModel();
            pkmManager = new PokedexManagerModel();

            dataManager = new DataManager();

            OnRefreshDataBase = new AsyncRelayCommand(() => OnLoadPokemonList(pkmToFind));

            //PerformSearch = new Command<string>((string query) => { SearchResults = GetSearchResults(query); });

            PerformSearch = new Command<string>(async query => await GetSearchResults(query));

            IsTapped = new Command<PokedexModel>(async p => await ItemTapped(p));

            OnClearData = new Command(ClearData);

            OnRefreshDataBase.Execute(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AsyncRelayCommand OnRefreshDataBase { get; private set; }

        public Command<PokedexModel> IsTapped { get; private set; }

        public Command OnClearData { get; private set; }

        public Command<string> PerformSearch { get; private set; }

        public ObservableCollection<PokedexModel> pkmList { get; private set; }

        public ObservableCollection<PokedexModel> SearchResults
        {
            get
            { 
                return searchResults; 
            }
            set
            {
                searchResults = value;
                OnPropertChanged("SearchResults");
            }
        }

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

        public async Task GetSearchResults(string queryString)
        {
            //var normalizedQuery = queryString?.ToLower() ?? "";
            //return pkmList.Where(p => p.ToLowerInvariant().Contains(normalizedQuery)).ToList();

            //ObservableCollection<PokedexModel> pokemon = new ObservableCollection<PokedexModel>(pkmList);
            //return (ObservableCollection<PokedexModel>)pkmList.Where(p => p.Name.StartsWith(queryString));

            //PokedexModel pk = pkmList.Where(p => p.Name.StartsWith(queryString)).First();
            PokedexModel pk = pkmList.Where(p  => p.Name == queryString.ToLower()).FirstOrDefault();
            
            if (pk == null)
            {
                return;
            }

            await ItemTapped(pk);
        }

        private void SearchPokemon()
        {

        }

        private void ClearData()
        {
            dataManager.RemovePokemonDataFile();
            OnRefreshDataBase.Execute(null);
        }
    }
}
