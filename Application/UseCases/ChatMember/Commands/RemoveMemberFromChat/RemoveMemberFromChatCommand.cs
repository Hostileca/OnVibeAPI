using Application.Dtos.Chat;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.RemoveMemberFromChat;

public sealed class RemoveMemberFromChatCommand : RequestBase<ChatReadDto>
{
    public Guid ChatId { get; init; }
    public Guid UserId { get; init; }
}
