using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using PokeDex.Models;

namespace PokeDex.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        PokedexManagerModel pkmManager;

        private string width;

        //private ObservableCollection<PokedexModel> _pkmList;

        public MainPageViewModel()
        {
            Width = "105";

            pkmManager = new PokedexManagerModel();
            pkmList = new ObservableCollection<PokedexModel>();

            Cmd = new AsyncRelayCommand(() => pkmManager.PopulatePokemonList(pkmList, 151));
        }

        public ObservableCollection<PokedexModel> pkmList { get; set; }

        //public ObservableCollection<PokedexModel> pkmList 
        //{ 
        //    get 
        //    { 
        //        return _pkmList; 
        //    } 
        //    set 
        //    { 
        //        _pkmList = value; 
        //        OnPropertChanged("pkmList"); 
        //    } 
        //}

        public string Header { get; set; } = "Search";

        public string Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertChanged("Width");
            }
        }

        public AsyncRelayCommand Cmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task PopulatePokemonList(int maxPkm)
        {
            for (int i = 1; i <= maxPkm; i++)
            {

                pkmList.Add(await pkmManager.CreatePkm($"{i}"));
            }
        }

        public async Task GetPokemonList(int maxPkm)
        {
            pkmList = new ObservableCollection<PokedexModel>(await pkmManager.PopulatePokemonList(maxPkm));
        }

        public async Task UpdatePokemonList(int maxPkm)
        {
            foreach (var pkm in await pkmManager.PopulatePokemonList(maxPkm))
            {
                pkmList.Add(pkm);
            }
        }

        private void OnPropertChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
