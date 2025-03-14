using Application.Dtos.User;
using MediatR;

namespace Application.UseCases.User.Commands.Register;

public sealed record RegisterCommand(string Email, string Username, string Password) : IRequest<UserReadDto>;