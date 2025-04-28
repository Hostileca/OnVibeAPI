using Application.Dtos.Post;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;

namespace Application.Services.Implementations.ExtraLoaders;

public class PostReadDtoExtraLoader(
    ILikeRepository likeRepository, 
    ICommentRepository commentRepository, 
    IAttachmentRepository attachmentRepository,
    IUserContext userContext) 
    : ExtraLoaderBase<PostReadDto>
{
    public override async Task LoadExtraInformationAsync(PostReadDto dto, CancellationToken cancellationToken = default)
    {
        dto.LikesCount = await likeRepository.GetPostLikesCountAsync(dto.Id, cancellationToken);
        dto.CommentsCount = await commentRepository.GetPostCommentsCountAsync(dto.Id, cancellationToken);
        dto.AttachmentsIds = await attachmentRepository.GetAttachmentsIdsByPostIdAsync(dto.Id, cancellationToken);
        dto.IsLiked = await likeRepository.IsLikeExistAsync(dto.Id, userContext.InitiatorId, cancellationToken);
    }
}