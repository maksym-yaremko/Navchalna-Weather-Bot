using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wtb.TelegramApi.Models;

namespace Wtb.TelegramApi
{
    public static class ConfigService
    {
        private const string CONFIG_FILE_NAME = "C:\\Users\\maksy\\Desktop\\config.json";

        public static Config GetConfig()
        {
            var json = File.ReadAllText(CONFIG_FILE_NAME);
            var config = JsonConvert.DeserializeObject<Config>(json);
            return config;
        }
    }
}
