using Application.Dtos.Reaction;
using Application.Services.Interfaces.Notification;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Reaction.Commands.UpsertReaction;

public class UpsertReactionCommandHandler(
    IMessageRepository messageRepository,
    IChatNotificationService chatNotificationService)
    : IRequestHandler<UpsertReactionCommand, Unit>
{
    public async Task<Unit> Handle(UpsertReactionCommand request, CancellationToken cancellationToken)
    {
        var message = await messageRepository.GetAvailableToUserMessageAsync(
            request.MessageId,
            request.InitiatorId,
            new MessageIncludes { IncludeReactions = true },
            cancellationToken,
            trackChanges: true);

        if (message is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Message), request.MessageId.ToString());
        }

        var existingReaction = message.Reactions.FirstOrDefault(r => r.SenderId == request.InitiatorId);

        if (existingReaction == null)
        {
            await AddNewReactionAsync(request, message, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(request.Emoji))
        {
            await UpdateReactionAsync(existingReaction, request.Emoji, message.ChatId, cancellationToken);
        }
        else
        {
            await RemoveReactionAsync(message, existingReaction, message.ChatId, cancellationToken);
        }

        return Unit.Value;
    }

    private async Task AddNewReactionAsync(UpsertReactionCommand request, Domain.Entities.Message message, CancellationToken cancellationToken)
    {
        var reaction = request.Adapt<Domain.Entities.Reaction>();
        message.Reactions.Add(reaction);
        await messageRepository.SaveChangesAsync(cancellationToken);

        await chatNotificationService.SendReactionToGroupAsync(
            reaction.Adapt<ReactionReadDto>(),
            message.ChatId,
            cancellationToken);
    }

    private async Task UpdateReactionAsync(Domain.Entities.Reaction reaction, string emoji, Guid chatId, CancellationToken cancellationToken)
    {
        reaction.Emoji = emoji;
        await messageRepository.SaveChangesAsync(cancellationToken);

        await chatNotificationService.SendReactionToGroupAsync(
            reaction.Adapt<ReactionReadDto>(),
            chatId,
            cancellationToken,
            isRemoved: true);
    }

    private async Task RemoveReactionAsync(Domain.Entities.Message message, Domain.Entities.Reaction reaction, Guid chatId, CancellationToken cancellationToken)
    {
        message.Reactions.Remove(reaction);
        await messageRepository.SaveChangesAsync(cancellationToken);

        await chatNotificationService.SendReactionToGroupAsync(
            reaction.Adapt<ReactionReadDto>(),
            chatId,
            cancellationToken);
    }
}