using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.Chat.Commands.DeleteChat;

public sealed class DeleteChatCommand : IRequest<ChatReadDto>
{
    public Guid ChatId { get; init; }
    public Guid InitiatorId { get; init; }
}