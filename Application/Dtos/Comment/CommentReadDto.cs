using Application.Dtos.User;

namespace Application.Dtos.Comment;

public class CommentReadDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid PostId { get; set; }
    public UserShortReadDto Sender { get; set; }
    public DateTime Date { get; set; }
}