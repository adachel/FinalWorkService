using MessageService.Abstraction;
using MessageService.DB;
using MessageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace FinalWorkTest
{
    internal class MockMessageService : IMessageRepo
    {
        public List<Message> Messages = new List<Message>()
        {
        new Message() { Text = "messageOne", 
                        FromUser = Guid.Parse("9388212b-262e-4f9b-92c8-0d44c97a4f7e"), 
                        ToUser = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6") }
        };

        public List<string> ReceiveMessage(Guid toUser)
        {
            using (var context = new MessageContext())
            {
                var messages = new List<string>();
                var messagesContext = Messages;
                foreach (var mess in messagesContext)
                {
                    if (mess.ToUser == toUser && mess.StatusId == StatusId.Send)
                    {
                        var a = mess.StatusId;
                        messages.Add($"Сообщение от {mess.FromUser}, текст сообщения: {mess.Text}");
                        mess.StatusId = StatusId.Received;
                    }
                }
                //_messageContext.SaveChanges();
                return messages;
            }
                //throw new NotImplementedException();
        }

        public void SendMessage(string text, Guid fromUser, Guid toUser)
        {
            using (var context = new MessageContext())
            {
                var newMassage = new Message { Text = text, FromUser = fromUser, ToUser = toUser };
                Messages.Add(newMassage);
                //_messageContext.SaveChanges();
            }
        }
    }
}
