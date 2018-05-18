using System;
using Newtonsoft.Json;

namespace Wtb.YahooApi.Models
{
    public class Condition
    {
        [JsonProperty("code")]
        public ConditionCodes Code { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("temp")]
        public string Temperature { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}