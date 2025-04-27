using Application.Dtos.Chat;
using Application.UseCases.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Chat.Commands.CreateChat;

public class CreateChatCommand : RequestBase<ChatReadDto>
{
    public string Name { get; init; }
    public IFormFile? Image { get; init; }
    public IList<Guid> UserIds { get; init; }
}