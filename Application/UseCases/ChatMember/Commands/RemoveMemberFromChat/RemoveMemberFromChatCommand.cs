using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.RemoveMemberFromChat;

public sealed class RemoveMemberFromChatCommand : IRequest<ChatReadDto>
{
    public Guid ChatId { get; init; }
    public Guid UserId { get; init; }
    public Guid InitiatorId { get; init; }
}
