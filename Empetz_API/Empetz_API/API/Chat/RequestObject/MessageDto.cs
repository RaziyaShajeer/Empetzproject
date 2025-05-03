namespace Empetz_API.API.Chat.RequestObject
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? FromName { get; set; }
        public string? ToName { get; set; }
        public string? Content { get; set; }
        public string? GroupName { get; set; }
        public DateTime? SentDate { get; set; } = DateTime.Now;

    }
}
