namespace Domain.Entities.Notifications;

public class MessageNotification : NotificationBase
{
    public Message Message { get; set; }
    public Guid MessageId { get; set; }
}