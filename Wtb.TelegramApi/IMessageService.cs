using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wtb.TelegramApi.Models;

namespace Wtb.TelegramApi
{
    public interface IMessageService
    {
        Message SendMessage(SendMessageModel message);
    }
}
