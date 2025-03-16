using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.RemoveMemberFromChat;

public sealed record RemoveMemberFromChatCommand(Guid ChatId, Guid UserId, Guid InitiatorId) : IRequest<ChatReadDto>;
