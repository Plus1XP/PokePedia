using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDex.Models
{
    class DataManager
    {
        DataModel dataModel;
        PokedexManagerModel pokedexManager;

        private readonly string folderPath;
        private readonly string dataFileName;
        public readonly string fileLocation;

        public DataManager()
        {
            dataModel = new DataModel();
            pokedexManager = new PokedexManagerModel();

            folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dataFileName = "PkmDB";
            fileLocation = Path.Combine(folderPath, dataFileName);
        }

        public async Task<List<PokedexModel>> LoadPokemonDataList(int PkmToFind)
        {
            if (dataModel.ConfirmFileExists(fileLocation))
            {
                return await VerifyExistingList(PkmToFind);
            }
            else
            {
                return await CreateNewPokemonList(PkmToFind);
            }
        }

        private async Task<List<PokedexModel>> VerifyExistingList(int PkmToFind)
        {
            List<PokedexModel> data = dataModel.LoadDataFromFile(fileLocation) as List<PokedexModel>;

            if (data.Count.Equals(PkmToFind))
            {
                return data;
            }
            else
            {
                data.Clear();
                data = await pokedexManager.GetPokemonList(PkmToFind);
                dataModel.SaveDataToFile(fileLocation, data);
                return data;
            }
        }

        private async Task<List<PokedexModel>> CreateNewPokemonList(int PkmToFind)
        {
            List<PokedexModel> data = await pokedexManager.GetPokemonList(PkmToFind);
            dataModel.SaveDataToFile(fileLocation, data);
            return data;
        }

        public void RemovePokemonDataFile()
        {
            dataModel.DeleteDataFile(fileLocation);
        }
    }
}
