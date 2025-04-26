using Application.Dtos.Page;
using Application.Dtos.User;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Mapster;
using MediatR;

namespace Application.UseCases.User.Queries.GetUsersBySearchCriteria;

public class GetUsersBySearchCriteriaQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersBySearchCriteriaQuery, PagedResponse<UserReadDto>>
{
    public async Task<PagedResponse<UserReadDto>> Handle(GetUsersBySearchCriteriaQuery request, CancellationToken cancellationToken)
    {
        var searchCriteria = request.Adapt<UsersSearchCriteria>();
        var pageInfo = request.PageData.Adapt<PageInfo>();

        var users = await userRepository.SearchUsersAsync(searchCriteria, pageInfo, cancellationToken);

        return new PagedResponse<UserReadDto>(users.Adapt<IList<UserReadDto>>(), pageInfo.PageNumber, pageInfo.PageSize);
    }
}