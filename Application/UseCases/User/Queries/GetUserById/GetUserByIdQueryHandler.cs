using Application.Dtos.User;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.User.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserReadDto>
{
    public async Task<UserReadDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.Id.ToString());
        }
        
        return user.Adapt<UserReadDto>();
    }
}