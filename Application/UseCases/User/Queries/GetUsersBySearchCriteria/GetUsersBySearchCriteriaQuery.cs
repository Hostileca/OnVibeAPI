using Application.Dtos.Page;
using Application.Dtos.User;
using MediatR;

namespace Application.UseCases.User.Queries.GetUsersBySearchCriteria;

public class GetUsersBySearchCriteriaQuery : IRequest<PagedResponse<UserReadDto>>
{
    public string? Username { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public PageData PageData { get; set; }
}