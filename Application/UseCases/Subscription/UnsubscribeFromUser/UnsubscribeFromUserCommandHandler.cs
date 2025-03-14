using Application.Dtos.Subscription;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Subscription.UnsubscribeFromUser;

public class UnsubscribeFromUserCommandHandler(
    ISubscriptionRepository subscriptionRepository) 
    : IRequestHandler<UnsubscribeFromUserCommand, SubscriptionReadDto>
{
    public async Task<SubscriptionReadDto> Handle(UnsubscribeFromUserCommand request, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.GetSubscriptionAsync(request.UserId, request.InitiatorId, cancellationToken);

        if (subscription is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Subscription), request.UserId.ToString());
        }
        
        subscriptionRepository.RemoveSubscriptionAsync(subscription, cancellationToken);
        await subscriptionRepository.SaveChangesAsync(cancellationToken);

        return subscription.Adapt<SubscriptionReadDto>();
    }
}