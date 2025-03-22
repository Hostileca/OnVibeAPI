namespace OnVibeAPI.Requests.Notification;

public class UpdateNotificationsRequest
{
    public bool IsRead { get; set; }
    public IList<Guid> NotificationIds { get; set; }
}