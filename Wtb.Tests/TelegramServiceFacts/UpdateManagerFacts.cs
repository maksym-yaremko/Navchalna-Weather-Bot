using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Ninject;
using Wtb.TelegramApi;
using Wtb.TelegramApi.Models;
using Xunit;

namespace Wtb.Tests.TelegramServiceFacts
{
    public class UpdateManagerFacts
    {
        private IUpdateManager _updateManager;
        private TestModule _module;

        public UpdateManagerFacts()
        {
            _module = new TestModule();
            var kernel = new StandardKernel(_module);

            _updateManager = kernel.Get<IUpdateManager>();

            _module.HttpManagerMock.Setup(ctx => ctx.Add<Message, SendMessageModel>(It.IsAny<string>(), It.IsAny<SendMessageModel>()))
                .Returns(new Message()
                {
                    MessageId = 543,
                    Chat = new Chat()
                    {
                        Id = 123
                    },
                    Text = "I'm Bot!"
                });
        }

        [Fact]
        public void GetUpdates()
        {
            var updates = _updateManager.GetUpdates();
            Assert.NotEqual(updates, null);
        }

        [Fact]
        public void ProcessUpdate()
        {
            var update = new Update()
            {
                UpdateId = 123,
                Message = new Message()
                {
                    Chat = new Chat()
                    {
                        Id = 456  
                    },
                    Text = "Hello"
                }
            };
            var response = (SendMessageModel)_updateManager.ProcessUpdate(update);

            Assert.Equal(response.ChatId, update.Message.Chat.Id);
            Assert.Equal(response.Text, "Hello, I'm WeatherBot! Have a nice day!");
        }

        [Fact]
        public void ProcessUpdate_StartCommand()
        {
            var update = new Update()
            {
                UpdateId = 123,
                Message = new Message()
                {
                    Chat = new Chat()
                    {
                        Id = 456
                    },
                    Text = "/start"
                }
            };
            var response = (SendMessageModel)_updateManager.ProcessUpdate(update);

            Assert.Equal(response.ChatId, update.Message.Chat.Id);
            Assert.True(response.Text.Contains("Hello, I'm WeatherBot. You can find out the current weather"));
        }

        [Fact]
        public void ProcessUpdate_WeatherCommand()
        {
            var update = new Update()
            {
                UpdateId = 123,
                Message = new Message()
                {
                    Chat = new Chat()
                    {
                        Id = 456
                    },
                    Text = "/weather Boston"
                }
            };
            var response = (BotResponse)_updateManager.ProcessUpdate(update);

            Assert.Equal(response.Command, BotCommands.Weather);
            Assert.Equal(response.City, "Boston");
        }

        [Fact]
        public void ProcessUpdate_ForecastCommand()
        {
            var update = new Update()
            {
                UpdateId = 123,
                Message = new Message()
                {
                    Chat = new Chat()
                    {
                        Id = 456
                    },
                    Text = "/forecast Boston"
                }
            };
            var response = (BotResponse)_updateManager.ProcessUpdate(update);

            Assert.Equal(response.Command, BotCommands.Forecast);
            Assert.Equal(response.City, "Boston");
        }

        [Fact]
        public void ProcessUpdate_HelpCommand()
        {
            var update = new Update()
            {
                UpdateId = 123,
                Message = new Message()
                {
                    Chat = new Chat()
                    {
                        Id = 456
                    },
                    Text = "/help"
                }
            };
            var response = (SendMessageModel) _updateManager.ProcessUpdate(update);

            Assert.Equal(response.ChatId, update.Message.Chat.Id);
            Assert.True(response.Text.Contains("/help - for getting the list of commands"));
        }
    }
}
