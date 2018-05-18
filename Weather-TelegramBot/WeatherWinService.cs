using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Wtb.TelegramApi;
using Wtb.TelegramApi.Models;

namespace Weather_TelegramBot
{
    class WeatherWinService
    {
        private const int TIMER_PERIOD = 5000;
        private Timer _timer;
        private ILog _logger;
        private ITelegramService _telegramService;

        private bool _startUpdateProcessing = false;

        public WeatherWinService(ITelegramService telegramService)
        {
            _logger = LogManager.GetLogger("WeatherBotService");
            _telegramService = telegramService;
            _telegramService.UpdatesProcessed += TelegramServiceOnUpdatesProcessed;
        }

        public void Start()
        {
            _logger.Debug($"WeatherBotService starting...");
            try
            {
                _timer = new Timer(TimerCallback, null, TIMER_PERIOD, TIMER_PERIOD);
            }
            catch (Exception ex)
            {
                _logger.Error($"Service crash", ex);
            }
        }

        public void Stop()
        {
            _logger.Debug($"WeatherBotService stopping...");
            try
            {
                _timer?.Dispose();
                _timer = null;
            }
            catch (Exception ex)
            {
                _logger.Error($"Service crash", ex);
            }
        }

        private void TimerCallback(object state)
        {
            _logger.Info($"Heartbeat!");
            if (!_startUpdateProcessing)
            {
                _startUpdateProcessing = true;
                _telegramService.CheckUpdates();
                _telegramService.UpdateProcessing();
            }
        }

        private void TelegramServiceOnUpdatesProcessed()
        {
            _startUpdateProcessing = false;
        }
    }
}
