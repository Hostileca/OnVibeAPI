using Application.Dtos.Like;
using Application.ExtraLoaders;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Like.Commands.RemoveLikeFromPost;

public class RemoveLikeFromPostCommandHandler(
    IPostRepository postRepository,
    ILikeRepository likeRepository,
    IExtraLoader<LikeReadDto> likeExtraLoader) 
    : IRequestHandler<RemoveLikeFromPostCommand, LikeReadDto>
{
    public async Task<LikeReadDto> Handle(RemoveLikeFromPostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Post), request.PostId.ToString());
        }
        
        var like = await likeRepository.GetLikeAsync(request.PostId, request.InitiatorId, cancellationToken);
        
        if (like is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Like), request.PostId.ToString());
        }
        
        likeRepository.RemoveLikeAsync(like);
        
        await likeRepository.SaveChangesAsync(cancellationToken);
        
        var likeReadDto = like.Adapt<LikeReadDto>();
        await likeExtraLoader.LoadExtraInformationAsync(likeReadDto, request.InitiatorId, cancellationToken);
        return likeReadDto;
    }
}