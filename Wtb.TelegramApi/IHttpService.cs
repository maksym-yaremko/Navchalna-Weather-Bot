using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wtb.TelegramApi
{
    public interface IHttpService
    {
        T Get<T>(string apiMethod);

        IEnumerable<T> GetList<T>(string apiMethod);

        TItem Add<TItem, TSpecialItem>(string apiMethod, TSpecialItem model);

        T Update<T>(string apiMethod, T model);

        void Delete<T>(string apiMethod, T model);
    }
}
