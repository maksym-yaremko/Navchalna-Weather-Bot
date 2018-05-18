using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Wtb.Helpers;
using Wtb.Helpers.UnitsConverter;
using Wtb.TelegramApi.Models;
using Wtb.YahooApi;
using Wtb.YahooApi.Models;

namespace Wtb.TelegramApi
{
    public class TelegramService : ITelegramService
    {
        private ILog _logger;
        private IUpdateManager _updateManager;
        private IMessageService _messageService;
        private IYahooWeatherService _weatherService;
        private IUnitsConverterFactory _unitsConverterFactory;

        private Queue<Update> _updates;

        public event Action UpdatesProcessed;

        public TelegramService(ILog logger, 
                               IUpdateManager updateManager, 
                               IMessageService messageService, 
                               IYahooWeatherService weatherService, 
                               IUnitsConverterFactory unitsConverterFactory)
        {
            _updateManager = updateManager;
            _messageService = messageService;
            _weatherService = weatherService;
            _unitsConverterFactory = unitsConverterFactory;
            _logger = logger;
            _logger.Debug($"Telegram Manager creation...");
            _updates = new Queue<Update>();
        }

        /// <summary>
        /// Check telegram updates
        /// </summary>
        public void CheckUpdates()
        {
            _logger.Debug($"Start update checking...");
            var updates = _updateManager.GetUpdates();
            foreach (var update in updates)
            {
                if (update != null)
                {
                    _updates.Enqueue(update);
                }
            }
            _logger.Debug($"Finish update checking...");
        }

        /// <summary>
        /// Update processing
        /// </summary>
        public void UpdateProcessing()
        {
            _logger.Debug($"Start update processing...");
            while (_updates.Count > 0)
            {
                var update = _updates.Dequeue();
                var response = _updateManager.ProcessUpdate(update);
                if (response.GetType() == typeof (BotResponse))
                {
                    var botResponse = (BotResponse) response;
                    var message = new SendMessageModel();
                    switch (botResponse.Command)
                    {
                        case BotCommands.Weather:
                            var weatherResponse = _weatherService.GetWeather(botResponse.City);
                            message = new SendMessageModel()
                            {
                                ChatId = botResponse.ReceiverId,
                                Text = GetWeatherMessage(weatherResponse)
                            };
                            if (message.Text != string.Empty)
                            {
                                _messageService.SendMessage(message);
                            }
                            break;
                        case BotCommands.Forecast:
                            var forecast = _weatherService.GetForecast(botResponse.City);
                            message = new SendMessageModel()
                            {
                                ChatId = botResponse.ReceiverId,
                                Text = GetForecastMessage(forecast)
                            };
                            if (message.Text != string.Empty)
                            {
                                _messageService.SendMessage(message);
                            }
                            break;
                        default:
                            _logger.Error($"Unknown bot command: {botResponse}");
                            break;
                    }
                }
                else
                {
                    _messageService.SendMessage((SendMessageModel)response);
                }
            }
            if (_updates.Count == 0)
            {
                _logger.Debug($"Finish update processing...");
                UpdatesProcessed();
            }
        }

        private string GetWeatherMessage(WeatherResponse weatherResponse)
        {
            var result = string.Empty;

            try
            {
                var response = weatherResponse.Query.Result.Channel;

                var temperature = response.Units.Temperature == "F"
                    ? $"{_unitsConverterFactory.Convert(ConverterType.Temperature, response.Item.Condition.Temperature)}°C"
                    : $"{response.Item.Condition.Temperature}°{response.Units.Temperature}";

                var wind = response.Units.Speed == "mph"
                    ? $"{_unitsConverterFactory.Convert(ConverterType.Speed, response.Wind.Speed)} km/h"
                    : $"{response.Wind.Speed} {response.Units.Speed}";

                var windDirection = _unitsConverterFactory.Convert(ConverterType.DegreeToDirection,
                    response.Wind.Direction);

                var pressure = $"{response.Atmosphere.Pressure} millibars";

                result =
                    $"Now: {temperature}\n\r" +
                    $"{response.Item.Condition.Text}\n\r" +
                    $"Wind: {wind} " +
                    $"{windDirection}\n\r" +
                    $"Pressure: {pressure}\n\r" +
                    $"Humidity: {response.Atmosphere.Humidity}%";
            }
            catch (Exception ex)
            {
                _logger.Error("Get weather message error.", ex);
            }

            return result;
        }

        private string GetForecastMessage(List<ForecastCondition> forecast)
        {
            var result = string.Empty;

            try
            {
                foreach (var item in forecast)
                {
                    var highTemperature =
                        $"{_unitsConverterFactory.Convert(ConverterType.Temperature, item.HighTemperature)}°C";

                    var lowTemperature =
                        $"{_unitsConverterFactory.Convert(ConverterType.Temperature, item.LowTemperature)}°C";

                    result +=
                        $"Date: {item.Date}\r\n" +
                        $"{item.Text}\r\n" +
                        $"High: {highTemperature}\r\n" +
                        $"Low: {lowTemperature}\r\n" +
                        "--------------------------\r\n";
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Get forecast message error.", ex);
            }

            return result;
        }
    }
}
