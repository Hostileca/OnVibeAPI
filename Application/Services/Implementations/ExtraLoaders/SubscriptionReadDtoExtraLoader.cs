using Application.Dtos.Subscription;
using Contracts.DataAccess.Interfaces;

namespace Application.Services.Implementations.ExtraLoaders;

public class SubscriptionReadDtoExtraLoader(
    ISubscriptionRepository subscriptionRepository) 
    : ExtraLoaderBase<SubscriptionReadDto>
{
    public override async Task LoadExtraInformationAsync(SubscriptionReadDto dto, CancellationToken cancellationToken = default)
    {
        dto.IsSubscribed = await subscriptionRepository.IsSubscriptionExistAsync(dto.SubscribedToId, dto.SubscribedById, cancellationToken);
    }
}