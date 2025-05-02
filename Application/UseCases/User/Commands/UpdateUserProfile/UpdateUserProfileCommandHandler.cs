using Application.Dtos.User;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
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
        var initiator = await userRepository.GetUserByIdAsync(request.InitiatorId, cancellationToken);
        if (initiator is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.InitiatorId.ToString());
        }

        if (request.InitiatorId != request.UserId && initiator.Role != Roles.Admin)
        {
            throw new ForbiddenException("You can't change someone else's profile");
        }

        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken, true);
        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }

        request.Adapt(user);
        await userRepository.SaveChangesAsync(cancellationToken);

        var userReadDto = user.Adapt<UserReadDto>();
        await userExtraLoader.LoadExtraInformationAsync(userReadDto, cancellationToken);
        
        return userReadDto;
    }
}