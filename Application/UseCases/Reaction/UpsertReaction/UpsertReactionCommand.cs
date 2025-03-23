using MediatR;

namespace Application.UseCases.Reaction.UpsertReaction;

public class UpsertReactionCommand : IRequest
{
    public Guid MessageId { get; set; }
    public Guid InitiatorId { get; set; }
    public string Emoji { get; set; }
}