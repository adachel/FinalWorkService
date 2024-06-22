namespace MessageService.Models
{
    public class Status
    {
        public StatusId StatusId { get; set; }
        public string Message { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
