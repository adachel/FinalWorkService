using MessageService.Abstraction;
using MessageService.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Abstractions;

namespace FinalWorkTest
{
    internal class TestMessageService
    {
        private MockMessageService _mockMessageRepo;
        [SetUp]
        public void Setup()
        {
            _mockMessageRepo = new MockMessageService();
        }

        [Test]
        public void SendMessageTest()
        {
            var message = new MessageViewModel() { Text = "messageTwo",
                                                   FromUser = Guid.Parse("9388212b-262e-4f9b-92c8-0d44c97a4f7e"),
                                                   ToUser = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

            _mockMessageRepo.SendMessage(message.Text, message.FromUser, message.ToUser);
            var res = _mockMessageRepo.Messages.FirstOrDefault(x => x.Text == "messageTwo");
            Assert.IsNotNull(res);
        }

        [Test]
        public void ReceiveMessageTest()
        {
            var toUser = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var res = _mockMessageRepo.ReceiveMessage(toUser);
            Assert.IsNotNull(res);
        }
    }
}
