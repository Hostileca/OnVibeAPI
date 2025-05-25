using MediatR;

namespace Application.UseCases.Reaction.Commands.UpsertReaction;

public class UpsertReactionCommand : IRequest<Unit>
{
    public Guid InitiatorId { get; init; }
    public Guid ChatId { get; init; }
    public Guid MessageId { get; init; }
    public string? Emoji { get; init; }
}