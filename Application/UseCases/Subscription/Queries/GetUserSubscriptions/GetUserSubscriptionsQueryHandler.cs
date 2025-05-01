using Application.Dtos.Page;
using Application.Dtos.Subscription;
using Application.Enums;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Subscription.Queries.GetUserSubscriptions;

public class GetUserSubscriptionsQueryHandler(
    IUserRepository userRepository,
    ISubscriptionRepository subscriptionRepository,
    IExtraLoader<SubscriptionReadDto> subReadDtoExtraLoader) 
    : IRequestHandler<GetUserSubscriptionsQuery, PagedResponse<SubscriptionReadDto>>
{
    public async Task<PagedResponse<SubscriptionReadDto>> Handle(GetUserSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }
        
        var subscribers = await subscriptionRepository.GetSubscriptionsAsync(
            request.UserId, 
            request.PageData.Adapt<PageInfo>(), 
            new SubscriptionIncludes
            {
                IncludeSubscribedTo = true
            },
            cancellationToken);
        
        var subscriptionsReadDtos = subscribers.Adapt<IList<SubscriptionReadDto>>();
        await subReadDtoExtraLoader.LoadExtraInformationAsync(subscriptionsReadDtos, cancellationToken);

        return new PagedResponse<SubscriptionReadDto>(subscriptionsReadDtos, request.PageData.PageNumber, request.PageData.PageSize);
    }
}