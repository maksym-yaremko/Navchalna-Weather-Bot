using Wtb.YahooApi.Models;

namespace Wtb.YahooApi
{
    public interface IHttpService
    {
        WeatherResponse Get(string query);
    }
}