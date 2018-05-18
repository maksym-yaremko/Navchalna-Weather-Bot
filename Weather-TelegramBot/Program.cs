using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Ninject;
using Topshelf;
using Wtb.TelegramApi;

namespace Weather_TelegramBot
{
    class Program
    {
        private static ILog _logger;
        private static string _serviceName;
        private static string _serviceDescription;
        private static int _startupTimeout;
        private static IKernel _kernel;
        private static ITelegramService _telegramService;

        static void Main(string[] args)
        {
            _kernel = new StandardKernel(new IocModule());
            _telegramService = _kernel.Get<ITelegramService>();
            _logger = LogManager.GetLogger("WeatherBotService");
            _serviceName = ConfigurationManager.AppSettings["ServiceName"];
            _serviceDescription = ConfigurationManager.AppSettings["ServiceDescription"];
            _startupTimeout = int.Parse(ConfigurationManager.AppSettings["StartupTimeout"]);

            try
            {
                HostFactory.Run(x =>
                {
                    x.Service<WeatherWinService>(s =>
                    {
                        s.ConstructUsing(name => new WeatherWinService(_telegramService));
                        s.WhenStarted(ws => ws.Start());
                        s.WhenStopped(ws => ws.Stop());
                    });

                    x.RunAsLocalSystem();
                    x.StartAutomaticallyDelayed();

                    x.SetDisplayName(_serviceName);
                    x.SetServiceName(_serviceName);
                    x.SetDescription(_serviceDescription);
                    x.SetStartTimeout(TimeSpan.FromSeconds(_startupTimeout));

                    x.EnableServiceRecovery(r =>
                    {
                        r.RestartService(1);
                        r.OnCrashOnly();
                        r.SetResetPeriod(0);
                    });

                    x.UseLog4Net("log4net.config");
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"Service crash", ex);
            }
        }
    }
}
