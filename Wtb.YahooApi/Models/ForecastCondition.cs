using Newtonsoft.Json;

namespace Wtb.YahooApi.Models
{
    public class ForecastCondition : Condition
    {
        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("high")]
        public string HighTemperature { get; set; }

        [JsonProperty("low")]
        public string LowTemperature { get; set; }
    }
}