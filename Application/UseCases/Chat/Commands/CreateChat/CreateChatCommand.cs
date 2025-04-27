using Application.Dtos.Chat;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Chat.Commands.CreateChat;

public class CreateChatCommand : IRequest<ChatReadDto>
{
    public Guid InitiatorId { get; init; }
    public string Name { get; init; }
    public IFormFile? Image { get; init; }
    public IList<Guid> UserIds { get; init; }
}