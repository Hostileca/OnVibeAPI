namespace Domain.Entities;

public class Like
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Post Post { get; set; }
    public Guid PostId { get; set; }
}