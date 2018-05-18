using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Ninject;
using Wtb.YahooApi;
using Wtb.YahooApi.Models;
using Xunit;

namespace Wtb.Tests.YahooWeatherServiceFacts
{
    public class YahooWeatherManagerFacts
    {
        private IYahooWeatherService _weatherService;
        private TestModule _module;

        public YahooWeatherManagerFacts()
        {
            _module = new TestModule();    
            var kernel = new StandardKernel(_module);

            _weatherService = kernel.Get<IYahooWeatherService>();

            _module.YahooHttpManagerMock.Setup(ctx => ctx.Get(It.IsAny<string>())).Returns(new WeatherResponse());
        }

        [Fact]
        public void GetWeather()
        {
            var city = "Chicago";

            var responce = _weatherService.GetWeather(city);

            _module.YahooHttpManagerMock.Verify(ctx=>ctx.Get(It.IsAny<string>()), Times.Once);
            Assert.NotEqual(responce, null);
        }
    }
}
