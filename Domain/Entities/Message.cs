using Domain.Entities.Notifications;

namespace Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; }
    public User? Sender { get; set; }
    public Guid? SenderId { get; set; }
    public Chat Chat { get; set; }
    public Guid ChatId { get; set; }
    public IList<Reaction> Reactions { get; set; } = new List<Reaction>();
    public IList<MessageAttachment> Attachments { get; set; } = new List<MessageAttachment>();
    public IList<MessageNotification> Notifications { get; set; }
}