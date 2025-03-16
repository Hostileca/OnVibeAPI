using Application.Dtos.Chat;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Chat.Commands.CreateChat;

public class CreateChatCommand : IRequest<ChatReadDto>
{
    public Guid InitiatorId { get; set; }
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
    public IList<Guid> UserIds { get; set; }
}