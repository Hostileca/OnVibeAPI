namespace OnVibeAPI.Requests.Chat;

public class UpdateChatRequest
{
    public string Name { get; set; }
    public IFormFile? Image { get; set; } 
}