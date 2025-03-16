using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetChatById;

public record GetChatByIdQuery(Guid ChatId, Guid InitiatorId) : IRequest<ChatReadDto>;