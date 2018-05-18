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
using Wtb.TelegramApi.Models;
using System.Configuration;

namespace Wtb.TelegramApi
{
    public class HttpService : IHttpService
    {
        private Config _config;
        private ILog _logger;
        private string _apiUrl;

        public HttpService()
        {
            _logger = LogManager.GetLogger("Telegram - HttpService");
            _config = ConfigService.GetConfig();
            _apiUrl = ConfigurationManager.AppSettings["TelegramApiUrl"];
        }

        public T Get<T>(string apiMethod)
        {
            try
            {
                var response = GetList<T>(apiMethod).ToList();
                return response[0];
            }
            catch (Exception ex)
            {
                _logger.Error($"Telegram HttpManager crash: Method: Get", ex);
            }
            return default(T);
        }

        public IEnumerable<T> GetList<T>(string apiMethod)
        {
            try
            {
                _logger.Debug($"Start request execution...");

                var url = String.Format(_apiUrl, _config.TelegramAccessToken, apiMethod);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                var json = string.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
                response.Close();

                _logger.Debug($"Get object: " + json);
                _logger.Debug($"Finish request execution...");

                var obj = JsonConvert.DeserializeObject<Result<T>>(json);
                return obj.Data;
            }
            catch (Exception ex)
            {
                _logger.Error($"Telegram HttpManager crash: Method: GetList", ex);
            }
            return null;
        }

        public TItem Add<TItem, TSpecialItem>(string apiMethod, TSpecialItem model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);

                _logger.Debug($"Start request execution...");
                _logger.Debug($"Send object: " + json);

                var url = String.Format(_apiUrl, _config.TelegramAccessToken, apiMethod);
                var postData = json;
                var data = Encoding.UTF8.GetBytes(postData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                json = string.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
                response.Close();

                _logger.Debug($"Get object: " + json);
                _logger.Debug($"Finish request execution...");

                var obj = JsonConvert.DeserializeObject<SingleResult<TItem>>(json);
                return obj.Data;
            }
            catch (Exception ex)
            {
                _logger.Error($"Telegram HttpManager crash: Method: Add", ex);
            }
            return default(TItem);
        }

        public T Update<T>(string apiMethod, T model)
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(string apiMethod, T model)
        {
            throw new NotImplementedException();
        }
    }
}
