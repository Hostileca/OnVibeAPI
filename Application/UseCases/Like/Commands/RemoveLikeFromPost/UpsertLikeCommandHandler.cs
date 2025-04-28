using Application.Dtos.Like;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Like.Commands.RemoveLikeFromPost;

public class UpsertLikeCommandHandler(
    IPostRepository postRepository,
    ILikeRepository likeRepository,
    IExtraLoader<LikeReadDto> likeExtraLoader)
    : IRequestHandler<UpsertLikeCommand, LikeReadDto>
{
    public async Task<LikeReadDto> Handle(UpsertLikeCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Post), request.PostId.ToString());
        }
        
        var like = await likeRepository.GetLikeAsync(request.PostId, request.InitiatorId, cancellationToken);
        
        if (like is null)
        {
            like = request.Adapt<Domain.Entities.Like>();
            await likeRepository.AddLikeAsync(like, cancellationToken);
        }
        else
        {
            likeRepository.RemoveLikeAsync(like);
        }
        
        await likeRepository.SaveChangesAsync(cancellationToken);
        
        var likeReadDto = like.Adapt<LikeReadDto>();
        await likeExtraLoader.LoadExtraInformationAsync(likeReadDto, cancellationToken);
        
        return likeReadDto;
    }
}