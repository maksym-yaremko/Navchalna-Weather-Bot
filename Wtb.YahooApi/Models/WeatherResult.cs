using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wtb.YahooApi.Models
{
    public class WeatherResult
    {
        [JsonProperty("channel")]
        public WeatherChannel Channel { get; set; }
    }
}
