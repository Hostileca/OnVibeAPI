using MediatR;

namespace Application.UseCases.Chat.Queries.GetChatImage;

public record GetChatImageQuery(Guid ChatId, Guid InitiatorId) : IRequest<byte[]>;