using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wtb.TelegramApi.Models
{
    public class SingleResult<T>
    {
        [JsonProperty("result")]
        public T Data { get; set; }
    }
}
