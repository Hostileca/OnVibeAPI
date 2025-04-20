using Application.Dtos.Chat;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Chat.Commands.UpdateChat;

public record UpdateChatCommand(Guid ChatId, Guid InitiatorId, string? Name, IFormFile? Image) : IRequest<ChatReadDto>;