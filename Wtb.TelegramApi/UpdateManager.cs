using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using log4net;
using Wtb.TelegramApi.Models;

namespace Wtb.TelegramApi
{
    public class UpdateManager : IUpdateManager
    {
        private ILog _logger;
        private IHttpService _httpService;

        private Hashtable _messagePatterns;

        private int _offset;

        public UpdateManager(IHttpService httpService)
        {
            _logger = LogManager.GetLogger("UpdateManager");
            _httpService = httpService;
            _offset = -1;

            _messagePatterns = MessagePatternStorage.GetPatterns();
        }

        public List<Update> GetUpdates()
        {
            try
            {
                var updates = _httpService.GetList<Update>("getUpdates?timeout=3000&offset=" + _offset);
                return updates.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Update manager crash: Method: GetUpdates", ex);
            }
            return null;
        }

        public object ProcessUpdate(Update update)
        {
            try
            {
                _offset = update.UpdateId + 1;
                var command = GetCommand(update.Message.Text);
                if (_messagePatterns.ContainsKey(command))
                {
                    var response = _messagePatterns[command];
                    if (response.GetType() == typeof (BotCommands))
                    {
                        return new BotResponse()
                        {
                            ReceiverId = update.Message.Chat.Id,
                            Command = (BotCommands)response,
                            City = GetCityName(update.Message.Text, (BotCommands)response)
                        };
                    }
                    else
                    {
                        var message = new SendMessageModel()
                        {
                            ChatId = update.Message.Chat.Id,
                            Text = response.ToString()
                        };
                        return message;
                    }
                }
                else
                {
                    return new SendMessageModel()
                    {
                        ChatId = update.Message.Chat.Id,
                        Text = "Hello, I'm WeatherBot! Have a nice day!"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateManager crash: Method ProcessUpdate", ex);
            }
            return null;
        }

        private string GetCommand(string message)
        {
            var pattern = "\\/\\w+(\\s|$)";
            var regex = new Regex(pattern);
            if (regex.IsMatch(message))
            {
                Match match = regex.Match(message);
                return match.Groups[0].Value.Trim();
            }
            return string.Empty;
        }

        private string GetCityName(string message, BotCommands command)
        {
            switch (command)
            {
                case BotCommands.Weather:
                    message = message.Replace("/weather", string.Empty).Trim();
                    return message;
                case BotCommands.Forecast:
                    message = message.Replace("/forecast", string.Empty).Trim();
                    return message;
            }
            return string.Empty;
        }
    }
}
