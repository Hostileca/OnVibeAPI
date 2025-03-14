namespace Domain.Entities;

public class Reaction
{
    public Guid Id { get; set; }
    public string Emoji { get; set; }
    public User Sender { get; set; }
    public Guid SenderId { get; set; }
    public Message Message { get; set; }
    public Guid MessageId { get; set; }
}