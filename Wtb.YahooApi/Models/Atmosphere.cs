using Newtonsoft.Json;

namespace Wtb.YahooApi.Models
{
    public class Atmosphere
    {
        [JsonProperty("humidity")]
        public string Humidity { get; set; }

        [JsonProperty("pressure")]
        public string Pressure { get; set; }

        [JsonProperty("rising")]
        public string Rising { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }
    }
}