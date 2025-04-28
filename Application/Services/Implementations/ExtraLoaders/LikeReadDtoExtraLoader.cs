using Application.Dtos.Like;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;

namespace Application.Services.Implementations.ExtraLoaders;

public class LikeReadDtoExtraLoader(
    ILikeRepository likeRepository,
    IUserContext userContext) 
    : ExtraLoaderBase<LikeReadDto>
{
    public override async Task LoadExtraInformationAsync(LikeReadDto dto, CancellationToken cancellationToken = default)
    {
        dto.LikesCount = await likeRepository.GetPostLikesCountAsync(dto.PostId, cancellationToken);
        dto.IsLiked = await likeRepository.IsLikeExistAsync(dto.PostId, userContext.InitiatorId, cancellationToken);
    }
}