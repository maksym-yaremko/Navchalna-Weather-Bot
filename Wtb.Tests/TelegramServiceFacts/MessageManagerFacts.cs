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
    public class MessageManagerFacts
    {
        private IMessageService _messageService;
        private TestModule _module;

        public MessageManagerFacts()
        {
            _module = new TestModule();
            var kernel = new StandardKernel(_module);

            _messageService = kernel.Get<IMessageService>();

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
        public void AddMessage()
        {
            var message = new SendMessageModel()
            {
                ChatId = 123,
                Text = "Hello!"
            };
            var response = _messageService.SendMessage(message);

            Assert.Equal(message.ChatId, response.Chat.Id);
            Assert.Equal(response.MessageId, 543);
            Assert.Equal(response.Text, "I'm Bot!");
        }
    }
}
