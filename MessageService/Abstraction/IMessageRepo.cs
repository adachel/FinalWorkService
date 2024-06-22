using MessageService.Models;

namespace MessageService.Abstraction
{
    public interface IMessageRepo
    {
        public void SendMessage(string text, Guid fromUser, Guid toUser);

        public List<string> ReceiveMessage(Guid toUser);
    }
}
