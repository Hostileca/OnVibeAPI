using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Reaction.UpsertReaction;

public class UpsertReactionCommand : RequestBase<Unit>
{
    public Guid MessageId { get; init; }
    public string Emoji { get; init; }
}