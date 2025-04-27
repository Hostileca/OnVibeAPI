using Application.Dtos.User;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.User.Queries.GetUserById;

public sealed class GetUserByIdQuery : RequestBase<UserReadDto>
{
    public Guid Id { get; init; }
}