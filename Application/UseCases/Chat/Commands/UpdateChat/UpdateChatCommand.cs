using Application.Dtos.Chat;
using Application.UseCases.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Chat.Commands.UpdateChat;

public class UpdateChatCommand : RequestBase<ChatReadDto>
{
    public Guid ChatId { get; init; }
    public string? Name { get; init; }
    public IFormFile? Image { get; init; }
}