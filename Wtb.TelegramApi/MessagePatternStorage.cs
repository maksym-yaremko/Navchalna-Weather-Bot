using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wtb.TelegramApi.Models;

namespace Wtb.TelegramApi
{
    public static class MessagePatternStorage
    {
        private static Hashtable _messagePatterns;

        public static Hashtable GetPatterns()
        {
            _messagePatterns = new Hashtable();
            SetPatterns();
            return _messagePatterns;
        }

        private static void SetPatterns()
        {
            var welcomeMessage =
                "Hello, I'm WeatherBot. You can find out the current weather and the forecast for next few day. Just use the next commands:\r\n";
            var commands = "/weather city - for find out current weather\r\n" +
                           "/forecast city - for find out the forecast for next few day\r\n" +
                           "/help - for getting the list of commands\r\n" +
                           "/aboutUs - about creators";

            var AboutUs = "Bot creators Bogdan Bondarets and Maksym Yaremko - students of LNU faculty of Applied Mathematic and Informatic";

            _messagePatterns.Add("/start", welcomeMessage + commands);
            _messagePatterns.Add("/weather", BotCommands.Weather);
            _messagePatterns.Add("/forecast", BotCommands.Forecast);
            _messagePatterns.Add("/aboutUs", AboutUs);
            _messagePatterns.Add("/help", commands);
        }
    }
}
