using Application.Dtos.Page;
using Application.Dtos.User;
using Application.ExtraLoaders;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Mapster;
using MediatR;

namespace Application.UseCases.User.Queries.GetUsersBySearchCriteria;

public class GetUsersBySearchCriteriaQueryHandler(
    IUserRepository userRepository,
    IExtraLoader<UserReadDto> userExtraLoader) 
    : IRequestHandler<GetUsersBySearchCriteriaQuery, PagedResponse<UserReadDto>>
{
    public async Task<PagedResponse<UserReadDto>> Handle(GetUsersBySearchCriteriaQuery request, CancellationToken cancellationToken)
    {
        var searchCriteria = request.Adapt<UsersSearchCriteria>();
        var pageInfo = request.PageData.Adapt<PageInfo>();

        var users = await userRepository.SearchUsersAsync(searchCriteria, pageInfo, cancellationToken);

        var usersReadDtos = users.Adapt<IList<UserReadDto>>();
        await userExtraLoader.LoadExtraInformationAsync(usersReadDtos, cancellationToken);
        
        return new PagedResponse<UserReadDto>(usersReadDtos, pageInfo.PageNumber, pageInfo.PageSize);
    }
}