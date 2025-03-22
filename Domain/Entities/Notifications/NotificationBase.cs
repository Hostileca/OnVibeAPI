namespace Domain.Entities.Notifications;

public abstract class NotificationBase
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
}