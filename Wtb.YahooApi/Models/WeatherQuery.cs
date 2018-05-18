using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wtb.YahooApi.Models
{
    public class WeatherQuery
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public WeatherResult Result { get; set; }
    }
}
