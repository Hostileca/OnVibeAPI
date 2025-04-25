using Application.Helpers;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.User.Queries.GetUserAvatar;

public class GetUserAvatarQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserAvatarQuery, byte[]>
{
    public async Task<byte[]> Handle(GetUserAvatarQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        
        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }
        
        if (user.Avatar is null)
        {
            throw new NotFoundException("Image is null");
        }
        
        return user.Avatar is null ? [] : Base64Converter.ConvertToByteArray(user.Avatar);
    }
}