using Application.Dtos.Page;
using Application.Dtos.Post;
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
    IAttachmentRepository attachmentRepository) 
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
            request.Page.Adapt<PageInfo>(), 
            cancellationToken);

        var response = new PagedResponse<PostReadDto>(
            posts.Adapt<IList<PostReadDto>>(), 
            request.Page.PageNumber,
            request.Page.PageSize);

        await LoadExtraInfoAsync(response, cancellationToken); 
        
        return response;
    }

    private async Task LoadExtraInfoAsync(PagedResponse<PostReadDto> response, CancellationToken cancellationToken)
    {
        foreach (var postReadDto in response.Items)
        {
            postReadDto.LikesCount = await postRepository.GetPostLikesCountAsync(postReadDto.Id, cancellationToken);
            postReadDto.CommentsCount = await postRepository.GetPostCommentsCountAsync(postReadDto.Id, cancellationToken);
            postReadDto.AttachmentsIds = await attachmentRepository.GetAttachmentsIdsByPostIdAsync(postReadDto.Id, cancellationToken);
        }
    }
}