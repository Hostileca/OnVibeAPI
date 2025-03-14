using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.Chat.Commands.CreateChat;

public class CreateChatCommand : IRequest<ChatReadDto>
{
    public Guid InitiatorId { get; set; }
    public string Name { get; set; }
    public IList<Guid> UserIds { get; set; }
}