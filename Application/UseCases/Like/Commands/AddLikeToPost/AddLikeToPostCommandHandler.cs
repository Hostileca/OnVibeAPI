using Application.Dtos.Like;
using Application.ExtraLoaders;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Like.Commands.AddLikeToPost;

public class AddLikeToPostCommandHandler(
    IPostRepository postRepository,
    ILikeRepository likeRepository,
    IExtraLoader<LikeReadDto> likeExtraLoader) 
    : IRequestHandler<AddLikeToPostCommand, LikeReadDto>
{
    public async Task<LikeReadDto> Handle(AddLikeToPostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Post), request.PostId.ToString());
        }

        if (await likeRepository.IsLikeExistAsync(request.PostId, request.InitiatorId, cancellationToken))
        {
            throw new ConflictException(typeof(Domain.Entities.Like), request.PostId.ToString());
        }

        var like = new Domain.Entities.Like
        {
            PostId = request.PostId,
            UserId = request.InitiatorId
        };
        await likeRepository.AddLikeAsync(like, cancellationToken);
        await likeRepository.SaveChangesAsync(cancellationToken);

        var likeReadDto = like.Adapt<LikeReadDto>();
        await likeExtraLoader.LoadExtraInformationAsync(likeReadDto, cancellationToken);
        
        return likeReadDto;
    }
}