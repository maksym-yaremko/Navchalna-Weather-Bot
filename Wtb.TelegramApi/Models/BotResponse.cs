using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wtb.TelegramApi.Models
{
    public class BotResponse
    {
        public int ReceiverId { get; set; }

        public string City { get; set; }

        public BotCommands Command { get; set; }
    }
}
