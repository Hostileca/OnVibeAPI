using Application.Dtos.Like;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Like.Commands.AddLikeToPost;

public class AddLikeToPostCommandHandler(
    IPostRepository postRepository,
    ILikeRepository likeRepository) 
    : IRequestHandler<AddLikeToPostCommand, LikeReadDto>
{
    public async Task<LikeReadDto> Handle(AddLikeToPostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Post), request.PostId.ToString());
        }

        if (await likeRepository.IsLikeExistAsync(request.PostId, request.UserId, cancellationToken))
        {
            throw new ConflictException(typeof(Domain.Entities.Like), request.PostId.ToString());
        }

        var like = new Domain.Entities.Like
        {
            PostId = request.PostId,
            UserId = request.UserId
        };
        await likeRepository.AddLikeAsync(like, cancellationToken);
        await likeRepository.SaveChangesAsync(cancellationToken);

        return like.Adapt<LikeReadDto>();
    }
}