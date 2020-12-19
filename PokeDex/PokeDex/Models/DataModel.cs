using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

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

        public Object LoadDataFromFile(string file)
        {
            return JsonConvert.DeserializeObject<List<PokedexModel>>(File.ReadAllText(file));
        }

        public void SaveDataToFile(string file, Object data)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(data));
        }

        public void DeleteDataFile(string file)
        {
            File.Delete(file);
        }
    }
}
