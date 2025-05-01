using Application.Dtos.Page;
using Application.Dtos.Subscription;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Subscription.Queries.GetUserSubscribers;

public class GetUserSubscribersQueryHandler(
    IUserRepository userRepository,
    ISubscriptionRepository subscriptionRepository,
    IExtraLoader<SubscriptionReadDto> subReadDtoExtraLoader) 
    : IRequestHandler<GetUserSubscribersQuery, PagedResponse<SubscriptionReadDto>>
{
    public async Task<PagedResponse<SubscriptionReadDto>> Handle(GetUserSubscribersQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }
        
        var subscribers = await subscriptionRepository.GetSubscribersAsync(
            request.UserId, 
            request.PageData.Adapt<PageInfo>(), 
            new SubscriptionIncludes
            {
                IncludeSubscribedBy = true
            },
            cancellationToken);
        
        var subscribersReadDtos = subscribers.Adapt<IList<SubscriptionReadDto>>();
        await subReadDtoExtraLoader.LoadExtraInformationAsync(subscribersReadDtos, cancellationToken);

        return new PagedResponse<SubscriptionReadDto>(subscribersReadDtos, request.PageData.PageNumber, request.PageData.PageSize);
    }
}