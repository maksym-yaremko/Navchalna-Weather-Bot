using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wtb.TelegramApi.Models
{
    public class SendMessageModel
    {
        [JsonProperty("chat_id")]
        public int ChatId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
