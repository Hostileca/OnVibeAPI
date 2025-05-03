using Domain.Entities;

namespace Application.Services.Interfaces.Notification;

public interface IChatNotificationService
{
    Task SendMessageAsync(Message message, CancellationToken cancellationToken);
    Task RemoveMemberAsync(ChatMember member, CancellationToken cancellationToken);
}