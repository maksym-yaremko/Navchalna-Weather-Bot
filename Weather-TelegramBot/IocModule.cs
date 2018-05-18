using log4net;
using Ninject.Modules;
using Wtb.Helpers.UnitsConverter;
using Wtb.TelegramApi;
using Wtb.YahooApi;
using HttpService = Wtb.TelegramApi.HttpService;
using IHttpService = Wtb.TelegramApi.IHttpService;

namespace Weather_TelegramBot
{
    public class IocModule:NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger("telegramService")).InTransientScope();

            Bind<ITelegramService>().To<TelegramService>().InSingletonScope();
            Bind<IHttpService>().To<HttpService>().InTransientScope();
            Bind<IUpdateManager>().To<UpdateManager>().InTransientScope();
            Bind<IMessageService>().To<MessageService>().InTransientScope();
            Bind<Wtb.YahooApi.IHttpService>().To<Wtb.YahooApi.HttpService>().InTransientScope();
            Bind<IYahooWeatherService>().To <YahooWeatherService>().InTransientScope();
            Bind<IUnitsConverterFactory>().To<UnitsConverterFactory>().InTransientScope();
        }
    }
}