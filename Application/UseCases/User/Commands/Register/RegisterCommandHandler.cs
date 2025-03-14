using Application.Dtos.User;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.User.Commands.Register;

public class RegisterCommandHandler(
    IUserRepository userRepository,
    IHashingService hashingService) 
    : IRequestHandler<RegisterCommand, UserReadDto>
{
    public async Task<UserReadDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isEmailRegistered = await userRepository.IsEmailRegisteredAsync(request.Email, cancellationToken);

        if (isEmailRegistered)
        {
            throw new ConflictException(typeof(Domain.Entities.User), request.Email);
        }

        var user = request.Adapt<Domain.Entities.User>();
        user.HashedPassword = hashingService.HashPassword(request.Password);
        user.CreatedAt = DateTime.UtcNow;
        
        user = await userRepository.RegisterUserAsync(user, cancellationToken);
        
        await userRepository.SaveChangesAsync(cancellationToken);

        return user.Adapt<UserReadDto>();
    }
}