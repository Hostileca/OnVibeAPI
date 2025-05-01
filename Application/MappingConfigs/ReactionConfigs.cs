using Application.UseCases.Reaction.Commands.UpsertReaction;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class ReactionConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpsertReactionCommand, Reaction>()
            .Map(dest => dest.SenderId, src => src.InitiatorId);
    }
}