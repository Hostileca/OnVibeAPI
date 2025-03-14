using Application.Dtos.Subscription;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Subscription.SubscribeToUser;

public class SubscribeToUserCommandHandler(
    IUserRepository userRepository,
    ISubscriptionRepository subscriptionRepository) 
    : IRequestHandler<SubscribeToUserCommand, SubscriptionReadDto>
{
    public async Task<SubscriptionReadDto> Handle(SubscribeToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }

        if (await subscriptionRepository.IsSubscriptionExistAsync(request.UserId, request.InitiatorId,
                cancellationToken))
        {
            throw new ConflictException(typeof(Domain.Entities.Subscription), request.UserId.ToString());
        }

        var subscription = request.Adapt<Domain.Entities.Subscription>();
        
        await subscriptionRepository.AddSubscriptionAsync(subscription, cancellationToken);
        await subscriptionRepository.SaveChangesAsync(cancellationToken);

        return subscription.Adapt<SubscriptionReadDto>();
    }
}