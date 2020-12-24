using PokeDex.Models;
using PokeDex.Views;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PokeDex.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private PokedexManagerModel pkmManager;

        private DataManager dataManager;

        private DetailsPage detailsPageView;

        private DetailsPageViewModel detailsPageViewModel;

        private ObservableCollection<PokedexModel> searchSuggestionsCollection;

        private int pkmToFind;

        private string logoPath;

        private string aboutTitle;

        private string aboutText;

        private bool isRefrshing;

        private bool isSearchResultsListVisible;

        public MainPageViewModel()
        {
            pkmToFind = 151;

            logoPath = $"Data/Misc/Logo.png";

            aboutTitle = $"PokePedia v0.1.1";

            aboutText = "https://github.com/aleuts \n\nRefresh DataBase?";

            isRefrshing = false;

            isSearchResultsListVisible = false;

            pkmManager = new PokedexManagerModel();

            dataManager = new DataManager();

            detailsPageView = new DetailsPage();

            detailsPageViewModel = new DetailsPageViewModel();

            RefreshDataBaseCommand = new Command(async () => await LoadPokemonList(pkmToFind));

            PerformSearchCommand = new Command<string>((string query) => { SearchSuggestionsCollection = GetSearchResults(query ?? string.Empty); });

            ItemTappedCommand = new Command<PokedexModel>(async p => await ItemTapped(p));

            GridFocusedCommand = new Command(SetSearchBarFocus);

            ShowAboutCommand = new Command(async () => await ShowAbout());

            RefreshDataBaseCommand.Execute(null);
        }

        public Command RefreshDataBaseCommand { get; }

        public Command<PokedexModel> ItemTappedCommand { get; }

        public Command GridFocusedCommand { get; }

        public Command ShowAboutCommand { get; }

        public Command<string> PerformSearchCommand { get; }

        public ObservableCollection<PokedexModel> pkmList { get; private set; }

        public ObservableCollection<PokedexModel> SearchSuggestionsCollection
        {
            get
            {
                return searchSuggestionsCollection;
            }
            set
            {
                searchSuggestionsCollection = value;
                OnPropertChanged("SearchSuggestionsCollection");
            }
        }

        public bool IsSearchResultsListVisible
        {
            get
            {
                return isSearchResultsListVisible;
            }
            set
            {
                isSearchResultsListVisible = value;
                OnPropertChanged("IsSearchResultsListVisible");
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return isRefrshing;
            }
            set
            {
                isRefrshing = value;
                OnPropertChanged("IsRefreshing");
            }
        }

        public string SearchBoxPlaceHolder { get; private set; } = "Search Pokemon";

        public string Logo_Header => logoPath;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private ObservableCollection<PokedexModel> GetSearchResults(string query)
        {
            if (query.Length >= 1)
            {
                IsSearchResultsListVisible = true;
                //return new ObservableCollection<PokedexModel>(pkmList.Where(p => p.Name.ToLower().StartsWith(query.ToLower())));
                return new ObservableCollection<PokedexModel>(pkmList.Where(p => p.Name.ToLower().Contains(query.ToLower())));
            }
            else
            {
                IsSearchResultsListVisible = false;
                return new ObservableCollection<PokedexModel>();
            }
        }

        private async Task LoadPokemonList(int pkmToFind)
        {
            IsRefreshing = true;
            pkmList = new ObservableCollection<PokedexModel>(await dataManager.LoadPokemonDataList(pkmToFind));
            OnPropertChanged(null);
            IsRefreshing = false;
        }

        private async Task ItemTapped(PokedexModel pkm)
        {
            MessagingCenter.Send<MainPageViewModel, PokedexModel>(this, "Send_Selected_Pokemon", pkm);
            detailsPageView.BindingContext = detailsPageViewModel;
            await Application.Current.MainPage.Navigation.PushAsync(detailsPageView);
            IsSearchResultsListVisible = false;
        }

        private void SetSearchBarFocus()
        {
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            Debug.WriteLine($"Keyboard Dismissed");
        }

        private async Task ShowAbout()
        {
            bool hasExecutedRefresh;
            hasExecutedRefresh = await Application.Current.MainPage.DisplayAlert(aboutTitle, aboutText, "YES", "NO");
            ClearData(hasExecutedRefresh);
        }

        private void ClearData(bool hasExecutedRefresh)
        {
            if (hasExecutedRefresh)
            {
                IsRefreshing = true;
                dataManager.RemovePokemonDataFile();
                RefreshDataBaseCommand.Execute(null);
            }
        }
    }
}
