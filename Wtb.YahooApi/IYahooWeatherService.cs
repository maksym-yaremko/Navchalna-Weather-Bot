using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wtb.YahooApi.Models;

namespace Wtb.YahooApi
{
    public interface IYahooWeatherService
    {
        WeatherResponse GetWeather(string city);

        List<ForecastCondition> GetForecast(string city);
    }
}
