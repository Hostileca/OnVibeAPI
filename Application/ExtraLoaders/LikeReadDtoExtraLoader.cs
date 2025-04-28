using Application.Dtos.Like;
using Contracts.DataAccess.Interfaces;

namespace Application.ExtraLoaders;

public class LikeReadDtoExtraLoader(ILikeRepository likeRepository) : ExtraLoaderBase<LikeReadDto>
{
    public override async Task LoadExtraInformationAsync(LikeReadDto dto, CancellationToken cancellationToken = default)
    {
        dto.LikesCount = await likeRepository.GetPostLikesCountAsync(dto.PostId, cancellationToken);
    }
}