using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.Chat.Commands.LeaveChat;

public sealed record LeaveChatCommand(Guid ChatId, Guid UserId) : IRequest<ChatReadDto>;