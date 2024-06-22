namespace MessageService.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; } = Guid.Empty;

        public StatusId StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
