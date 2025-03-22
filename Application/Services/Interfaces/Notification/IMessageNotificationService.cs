using Domain.Entities;

namespace Application.Services.Interfaces.Notification;

public interface IMessageNotificationService
{
    Task SendMessageAsync(Message message, CancellationToken cancellationToken);
}