using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using Wtb.YahooApi.Models;

namespace Wtb.YahooApi
{
    public class HttpService : IHttpService
    {
        private ILog _logger;
        private string _apiUrl;

        public HttpService()
        {
            _logger = LogManager.GetLogger("Yahoo - HttpService");
            _apiUrl = ConfigurationManager.AppSettings["YahooApiUrl"];
        }

        public WeatherResponse Get(string query)
        {
            try
            {
                _logger.Debug($"Start request execution...");

                var url = $"{_apiUrl}?q={query}&format=json";
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                var json = string.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
                response.Close();

                _logger.Debug($"Get object: " + json);
                _logger.Debug($"Finish request execution...");

                var obj = JsonConvert.DeserializeObject<WeatherResponse>(json);
                return obj;
            }
            catch (Exception ex)
            {
                _logger.Error($"Yahoo HttpManager crash: Method: Get", ex);
            }
            return null;
        }
    }
}
