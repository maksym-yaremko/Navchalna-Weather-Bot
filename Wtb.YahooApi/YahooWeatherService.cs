using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using Wtb.YahooApi.Models;

namespace Wtb.YahooApi
{
    public class YahooWeatherService : IYahooWeatherService
    {
        private const int FORECAST_RESULTS_NUMBER = 5;
        private const string QUERY =
            "select * from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"{0}\")";

        private ILog _logger;
        private IHttpService _httpService;

        public YahooWeatherService(IHttpService httpService)
        {
            _logger = LogManager.GetLogger("YahooWeatherService");
            _httpService = httpService;
        }

        public WeatherResponse GetWeather(string city)
        {
            _logger.Debug("Weather getting...");
            var query = String.Format(QUERY, city);
            var response = _httpService.Get(query);
            return response;
        }

        public List<ForecastCondition> GetForecast(string city)
        {
            _logger.Debug("Forecast getting...");
            var query = String.Format(QUERY, city);
            var response = _httpService.Get(query);
            return response.Query.Result.Channel.Item.Forecast.GetRange(0, FORECAST_RESULTS_NUMBER);
        }
    }
}
