using Application.Dtos.Subscription;
using Application.UseCases.Subscription.Commands.UpsertSubscription;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class SubscriptionConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpsertSubscriptionCommand, Subscription>()
            .Map(dest => dest.SubscribedById, src => src.InitiatorId)
            .Map(dest => dest.SubscribedToId, src => src.UserId);

        config.NewConfig<Subscription, SubscriptionReadDto>()
            .Map(dest => dest.User, src => src.SubscribedTo, src => src.SubscribedTo != null)
            .Map(dest => dest.User, src => src.SubscribedBy, src => src.SubscribedBy != null);
    }
}