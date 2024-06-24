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
                var toUserContext = _messageContext.Users.FirstOrDefault(x => x.Id == toUser);

                if (toUserContext != null)
                {
                    var mess = _messageContext.Messages.FirstOrDefault(x => x.Text == text && x.FromUser == fromUser && x.ToUser == toUser);

                    if (mess != null)
                    {
                        throw new Exception("Сообщение уже отправлено");
                    }
                    else
                    {
                        var newMassage = new Message { Text = text, FromUser = fromUser, ToUser = toUser };
                        _messageContext.Messages.Add(newMassage);
                        _messageContext.SaveChanges();
                    //}
                }
                else
                {
                    throw new Exception("такого получателя нет");
                }
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
                    if (mess.ToUser == toUser)
                    { 
                        messages.Add($"Сообщение от {mess.FromUser}, текст сообщения: {mess.Text}");
                    }
                }
                return messages;
            }
        }

    }
}
