using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.Chat.Commands.AddMemberToChat;

public record AddMemberToChatCommand(Guid ChatId, Guid UserId, Guid InitiatorId) : IRequest<ChatReadDto>;