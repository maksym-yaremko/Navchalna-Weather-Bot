using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wtb.YahooApi.Models
{
    public class Units
    {
        [JsonProperty("distance")]
        public string Distance { get; set; }

        [JsonProperty("pressure")]
        public string Pressure { get; set; }

        [JsonProperty("temperature")]
        public string Temperature { get; set; }

        [JsonProperty("speed")]
        public string Speed { get; set; }
    }
}
