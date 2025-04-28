using Application.Dtos.Page;
using Application.Dtos.Post;
using Application.ExtraLoaders;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Post.Queries.GetUserPosts;

public class GetUserPostsQueryHandler(
    IUserRepository userRepository,
    IPostRepository postRepository,
    IExtraLoader<PostReadDto> postExtraLoader) 
    : IRequestHandler<GetUserPostsQuery, PagedResponse<PostReadDto>>
{
    public async Task<PagedResponse<PostReadDto>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }
        
        var posts = await postRepository.GetPostsByUserIdAsync(
            request.UserId, 
            new PostIncludes
            {
                IncludeUser = true
            },
            request.PageData.Adapt<PageInfo>(), 
            cancellationToken);

        var postsReadDtos = posts.Adapt<IList<PostReadDto>>();
        await postExtraLoader.LoadExtraInformationAsync(postsReadDtos, cancellationToken);
        var response = new PagedResponse<PostReadDto>(
            postsReadDtos, 
            request.PageData.PageNumber,
            request.PageData.PageSize);

        return response;
    }
}