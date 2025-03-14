using Application.Dtos.Token;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.User.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepository, 
    IHashingService hashingService,
    ITokenService tokenService)
    : IRequestHandler<LoginCommand, TokensReadDto>
{
    public async Task<TokensReadDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email, cancellationToken, true);
        
        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.Email);
        }
        
        if (!hashingService.VerifyPassword(request.Password, user.HashedPassword))
        {
            throw new UnauthorizedException("Wrong password");
        }

        var tokens = tokenService.GenerateTokens(user, cancellationToken);
        user.RefreshToken = tokens.RefreshToken.Value;

        await userRepository.SaveChangesAsync(cancellationToken);

        return tokens;
    }
}