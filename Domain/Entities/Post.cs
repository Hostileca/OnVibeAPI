namespace Domain.Entities;

public class Post
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public DateTime Date { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public IList<Comment> Comments { get; set; }
    public IList<Like> Likes { get; set; }
    public IList<PostAttachment> Attachments { get; set; }
}