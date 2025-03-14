using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.Chat.Commands.DeleteChat;

public sealed record DeleteChatCommand(Guid InitiatorId, Guid ChatId) : IRequest<ChatReadDto>;