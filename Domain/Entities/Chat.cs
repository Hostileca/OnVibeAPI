namespace Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    public IList<ChatMember> Members { get; set; }
    public IList<Message> Messages { get; set; }
}