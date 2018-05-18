using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wtb.TelegramApi.Models;

namespace Wtb.TelegramApi
{
    public interface ITelegramService
    {
        event Action UpdatesProcessed;

        /// <summary>
        /// Check telegram updates
        /// </summary>
        /// <returns>Updates</returns>
        void CheckUpdates();

        /// <summary>
        /// Update processing
        /// </summary>
        void UpdateProcessing();
    }
}
