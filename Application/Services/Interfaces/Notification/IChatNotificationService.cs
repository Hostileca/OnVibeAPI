using Application.Dtos.Chat;
using Application.Dtos.Message;
using Application.Dtos.Reaction;
using Domain.Entities;

namespace Application.Services.Interfaces.Notification;

public interface IChatNotificationService
{
    Task SendMessageToGroupAsync(MessageReadDto messageReadDto, CancellationToken cancellationToken);
    Task SendMessageReadToGroupAsync(Guid messageId, Guid userId, Guid chatId, CancellationToken cancellationToken);
    Task RemoveMemberFromGroupAsync(ChatMember member, CancellationToken cancellationToken);
    Task AddMemberToGroupAsync(ChatMember member, ChatReadDto chatReadDto, CancellationToken cancellationToken);
    Task AddMembersToGroupAsync(IEnumerable<ChatMember> members, ChatReadDto chatReadDto, CancellationToken cancellationToken);
    Task SendReactionToGroupAsync(ReactionReadDto reactionReadDto, Guid chatId, CancellationToken cancellationToken, bool isRemoved = false);
}