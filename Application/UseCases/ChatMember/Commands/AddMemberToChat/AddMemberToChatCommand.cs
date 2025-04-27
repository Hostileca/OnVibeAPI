using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.AddMemberToChat;

public class AddMemberToChatCommand : IRequest<ChatReadDto>
{
    public Guid ChatId { get; init; }
    public Guid UserId { get; init; }
    public Guid InitiatorId { get; init; }
}