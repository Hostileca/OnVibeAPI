using Application.Dtos.Message;
using Domain.Entities;

namespace Application.Services.Interfaces.Notification;

public interface IChatNotificationService
{
    Task SendMessageAsync(MessageReadDto messageReadDto, CancellationToken cancellationToken);
    Task RemoveMemberAsync(ChatMember member, CancellationToken cancellationToken);
}