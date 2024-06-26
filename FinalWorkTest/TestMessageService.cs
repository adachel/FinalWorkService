using MessageService.DB;
using MessageService.DTO;
using MessageService.Models;
using MessageService.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Repo;
using static System.Net.Mime.MediaTypeNames;

namespace FinalWorkTest
{
    public class TestMessageService
    {
        private List<Message> _messages = new List<Message>()
        {
        new Message() { Text = "MessageTextTest", FromUser = Guid.Parse("3db3f259-d930-4788-b9a1-3d72ff3df3e5"), ToUser = Guid.Parse("b1410410-efb0-43a8-aebb-2483ee14748f"), StatusId = StatusId.Send },
        };
        private string connectionString = "Host = localhost; Port=5432;Username=aaa;Password=1234;Database=FinalMessageTest";


        [SetUp]
        public void Setup()
        {
            using (var context = new MessageContext(connectionString))
            {
                context.Messages.RemoveRange(context.Messages);
                context.SaveChanges();

                foreach (var item in _messages)
                {
                    context.Messages.Add(item);
                }
                context.SaveChanges();
            }
        }

        [Test]
        public void SendMessageTest()
        {
            using (var context = new MessageContext(connectionString))
            {
                var message = new MessageViewModel() { Text = "New MessageTest", FromUser = Guid.Parse("3db3f259-d930-4788-b9a1-3d72ff3df3e5"), ToUser = Guid.Parse("b1410410-efb0-43a8-aebb-2483ee14748f") };

                var service = new MessageRepo(context);
                service.SendMessage("New MessageTest",  Guid.Parse("3db3f259-d930-4788-b9a1-3d72ff3df3e5"), Guid.Parse("b1410410-efb0-43a8-aebb-2483ee14748f"));

                var messageCount = context.Messages.Count();
                Assert.AreEqual(2, messageCount);
            }
        }

        [Test]
        public void ReceiveMessageTest()
        {
            using (var context = new MessageContext(connectionString))
            {
                
            }
        }

        [TearDown]
        public void Teardown()
        {
            using (var context = new MessageContext(connectionString))
            {
                context.Messages.RemoveRange(context.Messages);
                context.SaveChanges();
            }
        }
    }
}
