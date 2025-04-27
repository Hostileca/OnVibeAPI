using Application.Dtos.User;
using Application.ExtraLoaders;
using Contracts.DataAccess.Interfaces;
using Mapster;
using MediatR;

namespace Application.UseCases.User.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandHandler(
    IUserRepository userRepository,
    IExtraLoader<UserReadDto> userExtraLoader) 
    : IRequestHandler<UpdateUserProfileCommand, UserReadDto>
{
    public async Task<UserReadDto> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.Id, cancellationToken, true);

        request.Adapt(user);
        await userRepository.SaveChangesAsync(cancellationToken);

        var userReadDto = user.Adapt<UserReadDto>();

        await userExtraLoader.LoadExtraInformationAsync(userReadDto, cancellationToken);
        
        return userReadDto;
    }
}