using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wtb.TelegramApi.Models
{
    public class Result<T>
    {
        [JsonProperty("result")]
        public List<T> Data { get; set; }
    }
}
