namespace Domain.Entities;

public class ChatMember
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Chat Chat { get; set; }
    public Guid ChatId { get; set; }
    public DateTime JoinDate { get; set; }
    public ChatRole Role { get; set; } = ChatRole.Member;
}