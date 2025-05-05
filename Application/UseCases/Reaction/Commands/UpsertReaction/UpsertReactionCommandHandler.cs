using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Reaction.Commands.UpsertReaction;

public class UpsertReactionCommandHandler(
    IMessageRepository messageRepository) 
    : IRequestHandler<UpsertReactionCommand, Unit>
{
    public async Task<Unit> Handle(UpsertReactionCommand request, CancellationToken cancellationToken)
    {
        var message = await messageRepository.GetAvailableToUserMessageAsync(
            request.MessageId,
            request.InitiatorId,
            new MessageIncludes { IncludeReactions = true },
            cancellationToken,
            true);
        
        if (message is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Message), request.MessageId.ToString());
        }
        
        var reaction = message.Reactions.FirstOrDefault(r => r.SenderId == request.InitiatorId);
        
        if (reaction is null)
        {
            message.Reactions.Add(request.Adapt<Domain.Entities.Reaction>()); 
            await messageRepository.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
        
        if (request.Emoji is not null)
        {
            reaction.Emoji = request.Emoji;
        }
        else
        {
            message.Reactions.Remove(reaction);
        }
        
        await messageRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}