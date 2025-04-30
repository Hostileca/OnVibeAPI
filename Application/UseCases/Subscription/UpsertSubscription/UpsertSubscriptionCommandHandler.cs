using Application.Dtos.Subscription;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Subscription.UpsertSubscription;

public class UpsertSubscriptionCommandHandler(
    IUserRepository userRepository,
    ISubscriptionRepository subscriptionRepository,
    IExtraLoader<SubscriptionReadDto> subscriptionExtraLoader) 
    : IRequestHandler<UpsertSubscriptionCommand, SubscriptionReadDto>
{
    public async Task<SubscriptionReadDto> Handle(UpsertSubscriptionCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == request.InitiatorId)
        {
            throw new BadRequestException("You can't subscribe to yourself");
        }
        
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        
        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }

        var subscription = await subscriptionRepository.GetSubscriptionAsync(request.UserId, request.InitiatorId, cancellationToken);

        if (subscription is null)
        {
            subscription = request.Adapt<Domain.Entities.Subscription>();
            await subscriptionRepository.AddSubscriptionAsync(subscription, cancellationToken);
        }
        else
        {
            subscriptionRepository.RemoveSubscriptionAsync(subscription, cancellationToken);
        }

        await subscriptionRepository.SaveChangesAsync(cancellationToken);

        var subscriptionReadDto = subscription.Adapt<SubscriptionReadDto>();
        await subscriptionExtraLoader.LoadExtraInformationAsync(subscriptionReadDto, cancellationToken);
        
        return subscriptionReadDto;
    }
}