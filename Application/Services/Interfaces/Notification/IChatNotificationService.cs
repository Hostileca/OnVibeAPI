using Application.Dtos.Message;
using Domain.Entities;

namespace Application.Services.Interfaces.Notification;

public interface IChatNotificationService
{
    Task SendMessageToGroupAsync(MessageReadDto messageReadDto, CancellationToken cancellationToken);
    Task RemoveMemberFromGroupAsync(ChatMember member, CancellationToken cancellationToken);
    Task AddMemberToGroupAsync(ChatMember member, CancellationToken cancellationToken);
    Task AddMembersToGroupAsync(IEnumerable<ChatMember> members, CancellationToken cancellationToken);
}