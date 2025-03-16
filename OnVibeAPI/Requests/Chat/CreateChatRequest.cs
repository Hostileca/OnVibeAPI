namespace OnVibeAPI.Requests.Chat;

public class CreateChatRequest
{
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
    public IList<Guid> UserIds { get; set; }
}