using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace NwBotDiscord
{
    class Config
    {
        private const string configFolder = "Ressources";
        private const string configFile = "confing.json";

       public static Botconfig bot;

        static Config()
        {
            if (!Directory.Exists(configFolder))
               Directory.CreateDirectory(configFolder);

            if (!File.Exists(configFolder + "/" + configFile))
            {
                bot = new Botconfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);

            }
            else
            {
                string json = File.ReadAllText(configFolder + "/" + configFile);
                bot = JsonConvert.DeserializeObject<Botconfig>(json);
               
            }
           
        }
    }
    public struct Botconfig
    {
        public string token;
        public string cmdPrefix;
    }
}
