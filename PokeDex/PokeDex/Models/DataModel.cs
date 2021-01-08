using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

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
            return LoadDataFromXML(file).Equals(localData);
        }

        public object LoadDataFromXML(string file)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<PokedexModel>));
            using (StreamReader reader = new StreamReader(file))
            {
                return xml.Deserialize(reader);
            }
        }

        public void SaveDataToXML(string file, object data)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<PokedexModel>));
            using (StreamWriter writer = new StreamWriter(file))
            {
                xml.Serialize(writer, data);
            }
        }

        //public object LoadDataFromFile(string file)
        //{
        //    return JsonConvert.DeserializeObject<List<PokedexModel>>(File.ReadAllText(file));
        //}

        //public void SaveDataToFile(string file, object data)
        //{
        //    File.WriteAllText(file, JsonConvert.SerializeObject(data));
        //}

        public void DeleteDataFile(string file)
        {
            File.Delete(file);
        }
    }
}
