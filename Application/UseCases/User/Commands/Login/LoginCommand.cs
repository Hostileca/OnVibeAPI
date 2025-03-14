using Application.Dtos.Token;
using MediatR;

namespace Application.UseCases.User.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<TokensReadDto>;