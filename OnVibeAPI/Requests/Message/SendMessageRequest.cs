namespace OnVibeAPI.Requests.Message;

public class SendMessageRequest
{
    public Guid ChatId { get; set; }
    public string? Text { get; set; }
    public IList<IFormFile>? Attachments { get; set; }
    public DateTimeOffset? Delay { get; set; }
}