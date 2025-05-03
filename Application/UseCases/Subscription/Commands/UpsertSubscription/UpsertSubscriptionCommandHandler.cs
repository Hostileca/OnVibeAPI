using Application.Dtos.Subscription;
using Application.Dtos.User;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Subscription.Commands.UpsertSubscription;

public class UpsertSubscriptionCommandHandler(
    IUserRepository userRepository,
    ISubscriptionRepository subscriptionRepository,
    IExtraLoader<SubscriptionReadDto> subscriptionExtraLoader,
    IExtraLoader<UserReadDto> userExtraLoader) 
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

        var subscription = await subscriptionRepository.GetSubscriptionAsync(
            request.UserId, 
            request.InitiatorId, 
            new SubscriptionIncludes
            {
                IncludeSubscribedTo = true
            }, 
            cancellationToken);

        if (subscription is null)
        {
            subscription = request.Adapt<Domain.Entities.Subscription>();
            await subscriptionRepository.AddSubscriptionAsync(subscription, cancellationToken);
        }
        else
        {
            subscriptionRepository.RemoveSubscription(subscription);
        }

        await subscriptionRepository.SaveChangesAsync(cancellationToken);

        var subscriptionReadDto = subscription.Adapt<SubscriptionReadDto>();
        subscriptionReadDto.User = user.Adapt<UserReadDto>();
        await userExtraLoader.LoadExtraInformationAsync(subscriptionReadDto.User, cancellationToken);
        await subscriptionExtraLoader.LoadExtraInformationAsync(subscriptionReadDto, cancellationToken);
        
        return subscriptionReadDto;
    }
}