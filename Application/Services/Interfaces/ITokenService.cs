using Application.Dtos.Token;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ITokenService
{
    TokensReadDto GenerateTokens(User user, CancellationToken cancellationToken);
}