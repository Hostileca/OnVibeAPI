namespace OnVibeAPI.Requests.Post;

public class CreatePostRequest
{
    public string? Content { get; set; }
    public IList<IFormFile>? Attachments { get; set; }
}