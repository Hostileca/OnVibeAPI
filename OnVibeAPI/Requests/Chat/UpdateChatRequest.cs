namespace OnVibeAPI.Requests.Chat;

public class UpdateChatRequest
{
    public Guid ChatId { get; set; }
    public string Name { get; set; }
    public IFormFile? Image { get; set; } 
}