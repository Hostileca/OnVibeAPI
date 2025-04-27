using Application.Dtos.Chat;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Chat.Commands.UpdateChat;

public class UpdateChatCommand : IRequest<ChatReadDto>
{
    public Guid ChatId { get; init; }
    public Guid InitiatorId { get; init; }
    public string? Name { get; init; }
    public IFormFile? Image { get; init; }
}