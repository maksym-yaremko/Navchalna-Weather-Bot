using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wtb.YahooApi.Models
{
    public class WeatherItem
    {
        [JsonProperty("condition")]
        public Condition Condition { get; set; }

        [JsonProperty("forecast")]
        public List<ForecastCondition> Forecast { get; set; }
    }
}
