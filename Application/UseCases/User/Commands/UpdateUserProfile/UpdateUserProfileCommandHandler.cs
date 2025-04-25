using Application.Dtos.User;
using Contracts.DataAccess.Interfaces;
using Mapster;
using MediatR;

namespace Application.UseCases.User.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserProfileCommand, UserReadDto>
{
    public async Task<UserReadDto> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.Id, cancellationToken, true);

        request.Adapt(user);

        await userRepository.SaveChangesAsync(cancellationToken);
        
        return user.Adapt<UserReadDto>();
    }
}