using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace NwBotDiscord
{
    class DataStorage
    {
        private static Dictionary<string, string> pairs = new Dictionary<string, string>();
        // si on met internal au lieu de public static c'est uniquement cette application peut modifier
        public static void AddPairToStorage(string key, string value)
        {
            pairs.Add(key, value);
            Savedata();         
        }
        public static int GetPairCount()
        {
            return pairs.Count;
        }
        static DataStorage()
        {
            if (!ValidateStorageFile("DataStorage.json"))return;
            string json = File.ReadAllText("Datastorage.json");
            pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
        public static void Savedata()
        {
            string json = JsonConvert.SerializeObject(pairs, Formatting.Indented);
            File.WriteAllText("DataStorage.json", json);
        }

        public static bool ValidateStorageFile(string file)
        {
            if(!File.Exists(file))
            {
                File.WriteAllText(file, "");
                Savedata();
                return false;
            }
            return true;
        }
    }
}
