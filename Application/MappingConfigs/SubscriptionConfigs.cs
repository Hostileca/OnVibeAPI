using Application.UseCases.Subscription.SubscribeToUser;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class SubscriptionConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SubscribeToUserCommand, Subscription>()
            .Map(dest => dest.SubscribedById, src => src.InitiatorId)
            .Map(dest => dest.SubscribedToId, src => src.UserId);
    }
}