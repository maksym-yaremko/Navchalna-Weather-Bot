using System.Collections.Generic;
using Moq;
using Ninject;
using Wtb.TelegramApi;
using Wtb.TelegramApi.Models;
using Wtb.YahooApi.Models;
using Xunit;

namespace Wtb.Tests.IntegrationFacts
{
    public class TelegramYahooIntegrationFacts
    {
        private ITelegramService _telegramService;
        private TestModule _module;

        public TelegramYahooIntegrationFacts()
        {
            _module = new TestModule();
            var kernel = new StandardKernel(_module);

            _telegramService = kernel.Get<ITelegramService>();
            _telegramService.UpdatesProcessed += () =>
            {
                
            };
        }

        [Fact]
        public void SendWeatherMessage()
        {
            _module.HttpManagerMock.Setup(ctx => ctx.GetList<Update>(It.IsAny<string>())).Returns(() =>
            {
                var list = new List<Update>();
                list.Add(new Update()
                {
                    UpdateId = 123,
                    Message = new Message()
                    {
                        Chat = new Chat()
                        {
                            Id = 456
                        },
                        Text = "/weather Chicago"
                    }
                });
                return list;
            });

            _module.YahooHttpManagerMock.Setup(ctx => ctx.Get(It.IsAny<string>())).Returns(() =>
            {
                return new WeatherResponse()
                {
                    Query = new WeatherQuery()
                    {
                        Result = new WeatherResult()
                        {
                            Channel = new WeatherChannel()
                            {
                                Atmosphere = new Atmosphere()
                                {
                                    Humidity = "91",
                                    Pressure = "998.0"
                                },
                                Wind = new Wind()
                                {
                                    Speed = "11",
                                    Direction = "260"
                                },
                                Units = new Units()
                                {
                                    Distance = "mi",
                                    Pressure = "in",
                                    Speed = "mph",
                                    Temperature = "F"
                                },
                                Item = new WeatherItem()
                                {
                                    Condition = new Condition()
                                    {
                                        Temperature = "76",
                                        Text = "Mostly Cloudy"
                                    }
                                }
                            }
                        }
                    }
                };
            });

            _telegramService.CheckUpdates();

            _module.HttpManagerMock.Verify(ctx => ctx.GetList<Update>(It.IsAny<string>()), Times.Once);

            _telegramService.UpdateProcessing();

            _module.YahooHttpManagerMock.Verify(ctx => ctx.Get(It.IsAny<string>()), Times.Once);

            _module.HttpManagerMock.Verify(
                ctx => ctx.Add<Message, SendMessageModel>(It.IsAny<string>(), It.IsAny<SendMessageModel>()), Times.Once);
        }

        [Fact]
        public void SendForecastMessage()
        {
            _module.HttpManagerMock.Setup(ctx => ctx.GetList<Update>(It.IsAny<string>())).Returns(() =>
            {
                var list = new List<Update>();
                list.Add(new Update()
                {
                    UpdateId = 123,
                    Message = new Message()
                    {
                        Chat = new Chat()
                        {
                            Id = 456
                        },
                        Text = "/forecast Chicago"
                    }
                });
                return list;
            });

            var forecast = new List<ForecastCondition>();
            for (int i = 0; i < 10; i++)
            {
                forecast.Add(new ForecastCondition()
                {
                    Date = "28 Aug 2016",
                    HighTemperature = "78",
                    LowTemperature = "71",
                    Text = "Thunderstorms"
                });
            }

            _module.YahooHttpManagerMock.Setup(ctx => ctx.Get(It.IsAny<string>())).Returns(() =>
            {
                return new WeatherResponse()
                {
                    Query = new WeatherQuery()
                    {
                        Result = new WeatherResult()
                        {
                            Channel = new WeatherChannel()
                            {
                                Item = new WeatherItem()
                                {
                                    Forecast = forecast
                                }
                            }
                        }
                    }
                };
            });

            _telegramService.CheckUpdates();

            _module.HttpManagerMock.Verify(ctx => ctx.GetList<Update>(It.IsAny<string>()), Times.Once);

            _telegramService.UpdateProcessing();

            _module.YahooHttpManagerMock.Verify(ctx => ctx.Get(It.IsAny<string>()), Times.Once);

            _module.HttpManagerMock.Verify(
                ctx => ctx.Add<Message, SendMessageModel>(It.IsAny<string>(), It.IsAny<SendMessageModel>()), Times.Once);
        }
    }
}