using Domain.Entities.Notifications;

namespace Application.Dtos.Notification;

public class NotificationBaseReadDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; }
    public NotificationType Type { get; set; }
    public Guid UpdatedEntityId { get; set; }
}