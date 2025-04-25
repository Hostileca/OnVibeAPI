using Application.Dtos.User;
using MediatR;

namespace Application.UseCases.User.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserReadDto>;