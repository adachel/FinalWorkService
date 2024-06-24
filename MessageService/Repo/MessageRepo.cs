using AutoMapper;
using MessageService.Abstraction;
using MessageService.DB;
using MessageService.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MessageService.Repo
{
    public class MessageRepo : IMessageRepo
    {
        private readonly IMapper _mapper;
        private MessageContext _messageContext;
        public MessageRepo(IMapper mapper, MessageContext messageContext)
        {
            _mapper = mapper;
            _messageContext = messageContext;
        }

        public void SendMessage(string text, Guid fromUser, Guid toUser)
        {
            using (var context = new MessageContext())
            {
                var newMassage = new Message { Text = text, FromUser = fromUser, ToUser = toUser };
                _messageContext.Messages.Add(newMassage);
                _messageContext.SaveChanges();
            }
        }

        public List<string> ReceiveMessage(Guid toUser)
        {
            using (var context = new MessageContext())
            {
                var messages = new List<string>();
                var messagesContext = _messageContext.Messages;
                foreach (var mess in messagesContext)
                {
                    if (mess.ToUser == toUser && mess.StatusId == StatusId.Send)
                    {
                        var a = mess.StatusId;
                        messages.Add($"Сообщение от {mess.FromUser}, текст сообщения: {mess.Text}");
                        mess.StatusId = StatusId.Received;

                    }
                }
                _messageContext.SaveChanges();
                return messages;
            }
        }

    }
}
