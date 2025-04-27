using Application.Dtos.User;
using MediatR;

namespace Application.UseCases.User.Queries.GetUserById;

public sealed class GetUserByIdQuery : IRequest<UserReadDto>
{
    public Guid Id { get; init; }
}