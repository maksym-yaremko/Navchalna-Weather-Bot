using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Wtb.TelegramApi.Models;

namespace Wtb.TelegramApi
{
    public class MessageService : IMessageService
    {
        private IHttpService _httpService;
        private ILog _logger;

        public MessageService(IHttpService httpmanager)
        {
            _logger = LogManager.GetLogger("MessageService");
            _httpService = httpmanager;
        }

        public Message SendMessage(SendMessageModel message)
        {
            try
            {
                var response = _httpService.Add<Message, SendMessageModel>("sendMessage", message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error("Message Manager crash: Method: SendMessage", ex);
            }
            return null;
        }
    }
}
