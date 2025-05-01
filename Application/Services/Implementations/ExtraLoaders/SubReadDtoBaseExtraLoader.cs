using Application.Dtos.Subscription;
using Contracts.DataAccess.Interfaces;

namespace Application.Services.Implementations.ExtraLoaders;

public class SubReadDtoBaseExtraLoader(
    ISubscriptionRepository subscriptionRepository) 
    : ExtraLoaderBase<SubReadDtoBase>
{
    public override async Task LoadExtraInformationAsync(SubReadDtoBase dto, CancellationToken cancellationToken = default)
    {
        dto.IsSubscribed = await subscriptionRepository.IsSubscriptionExistAsync(dto.SubscribedToId, dto.SubscribedById, cancellationToken);
    }
}