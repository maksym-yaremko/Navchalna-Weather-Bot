using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Moq;
using Ninject.Modules;
using Wtb.TelegramApi;
using Wtb.TelegramApi.Models;
using Wtb.Tests.TelegramServiceFacts;
using Wtb.YahooApi;
using Wtb.Helpers.UnitsConverter;
using IHttpService = Wtb.TelegramApi.IHttpService;

namespace Wtb.Tests
{
    public class TestModule:NinjectModule
    {
        public Mock<IHttpService> HttpManagerMock { get; set; }
        public Mock<YahooApi.IHttpService> YahooHttpManagerMock { get; set; }
        public Mock<ILog> LoggerMock { get; set; }

        public TestModule()
        {
            HttpManagerMock = new Mock<IHttpService>();
            YahooHttpManagerMock = new Mock<YahooApi.IHttpService>();
            LoggerMock = new Mock<ILog>();
        }

        public override void Load()
        {
            Bind<IHttpService>().ToMethod(ctx => HttpManagerMock.Object).InTransientScope();
            Bind<YahooApi.IHttpService>().ToMethod(ctx => YahooHttpManagerMock.Object).InTransientScope();
            Bind<ILog>().ToMethod(ctx => LoggerMock.Object).InTransientScope();

            Bind<IUpdateManager>().To<UpdateManager>().InTransientScope();
            Bind<IMessageService>().To<MessageService>().InTransientScope();

            Bind<IYahooWeatherService>().To<YahooWeatherService>().InTransientScope();

            Bind<IUnitsConverterFactory>().To<UnitsConverterFactory>().InTransientScope();

            Bind<ITelegramService>().To<TelegramService>().InTransientScope();
        }
    }
}
