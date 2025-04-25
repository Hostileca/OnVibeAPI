using Application.Helpers;
using Application.UseCases.User.Commands.UpdateUserProfile;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class UserConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserProfileCommand, User>()
            .Map(dest => dest.Avatar, src => src.Avatar != null ? Base64Converter.ConvertToBase64(src.Avatar) : null);
    }
}