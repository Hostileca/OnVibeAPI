namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public string? RefreshToken { get; set; }
    public string Email { get; set; }
    public string? BIO { get; set; }
    public string? Avatar { get; set; }
    public DateTime CreatedAt { get; set; }
    public Roles Role { get; set; } = Roles.User;
    public IList<Message> Messages { get; set; }
    public IList<ChatMember> ChatsMembers { get; set; }
    public IList<Comment> Comments { get; set; }
    public IList<Reaction> Reactions { get; set; }
    public IList<Post> Posts { get; set; }
    public IList<Like> Likes { get; set; }
    public IList<Subscription> Subscriptions { get; set; }
    public IList<Subscription> Subscribers { get; set; }
}