using Application.Dtos.Chat;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Chat.Commands.UpdateChat;

public class UpdateChatCommand : IRequest<ChatReadDto>
{
    public Guid ChatId { get; set; }
    public string Name { get; set; }
    public IFormFile? Image { get; set; } 
    public Guid InitiatorId { get; set; }
}