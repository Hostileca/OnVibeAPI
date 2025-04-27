using Application.Dtos.Post;
using Contracts.DataAccess.Interfaces;

namespace Application.ExtraLoaders;

public class PostReadDtoExtraLoader(
    ILikeRepository likeRepository, 
    ICommentRepository commentRepository, 
    IAttachmentRepository attachmentRepository) 
    : ExtraLoaderBase<PostReadDto>
{
    public override async Task LoadExtraInformationAsync(PostReadDto dto, CancellationToken cancellationToken)
    {
        dto.LikesCount = await likeRepository.GetPostLikesCountAsync(dto.Id, cancellationToken);
        dto.CommentsCount = await commentRepository.GetPostCommentsCountAsync(dto.Id, cancellationToken);
        dto.AttachmentsIds = await attachmentRepository.GetAttachmentsIdsByPostIdAsync(dto.Id, cancellationToken);
    }
}