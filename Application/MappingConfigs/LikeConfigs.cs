using Application.UseCases.Like.Commands.UpsertLike;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class LikeConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpsertLikeCommand, Like>()
            .Map(dest => dest.UserId, src => src.InitiatorId);
    }
}