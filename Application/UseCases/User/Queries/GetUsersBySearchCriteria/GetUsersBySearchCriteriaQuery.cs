using Application.Dtos.Page;
using Application.Dtos.User;
using MediatR;

namespace Application.UseCases.User.Queries.GetUsersBySearchCriteria;

public class GetUsersBySearchCriteriaQuery : IRequest<PagedResponse<UserReadDto>>
{
    public string? Username { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    public PageData PageData { get; init; }
}