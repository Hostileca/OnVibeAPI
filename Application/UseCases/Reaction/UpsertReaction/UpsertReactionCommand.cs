using MediatR;

namespace Application.UseCases.Reaction.UpsertReaction;

public class UpsertReactionCommand : IRequest<Unit>
{
    public Guid InitiatorId { get; init; }
    public Guid MessageId { get; init; }
    public string Emoji { get; init; }
}