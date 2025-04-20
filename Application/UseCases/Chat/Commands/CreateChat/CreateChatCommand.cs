using Application.Dtos.Chat;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Chat.Commands.CreateChat;

public record CreateChatCommand(Guid InitiatorId, string Name, IFormFile? Image, IList<Guid> UserIds) : IRequest<ChatReadDto>;