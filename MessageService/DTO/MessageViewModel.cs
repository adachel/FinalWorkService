namespace MessageService.DTO
{
    public class MessageViewModel
    {
        public string Text { get; set; }
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
    }
}
