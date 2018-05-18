using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wtb.YahooApi.Models
{
    public class WeatherChannel
    {
        [JsonProperty("units")]
        public Units Units { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("atmosphere")]
        public Atmosphere Atmosphere { get; set; }

        [JsonProperty("item")]
        public WeatherItem Item { get; set; }
    }
}
