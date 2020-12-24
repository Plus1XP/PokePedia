using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;

namespace PokeDex.Models
{
    class DataModel
    {
        public bool ConfirmFileExists(string file)
        {
            return File.Exists(file);
        }

        public bool VerfiyFileContents(string file, object localData)
        {
            return LoadDataFromFile(file).Equals(localData);
        }

        public object LoadDataFromFile(string file)
        {
            return JsonConvert.DeserializeObject<List<PokedexModel>>(File.ReadAllText(file));
        }

        public void SaveDataToFile(string file, object data)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(data));
        }

        public void DeleteDataFile(string file)
        {
            File.Delete(file);
        }
    }
}
