using Application.UseCases.User.Commands.Register;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class UserConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
    }
}