namespace OnVibeAPI.Requests.Comment;

public class SendCommentRequest
{
    public Guid PostId { get; set; }
    public string Content { get; set; }
}