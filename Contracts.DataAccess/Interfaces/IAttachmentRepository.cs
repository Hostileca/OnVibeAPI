using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IAttachmentRepository
{
    Task<IList<Guid>> GetAttachmentsIdsByPostIdAsync(Guid postId, CancellationToken cancellationToken);
    Task<IList<Guid>> GetAttachmentsIdsByMessageIdAsync(Guid messageId, CancellationToken cancellationToken);
    Task<PostAttachment?> GetPostAttachmentByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<MessageAttachment?> GetMessageAttachmentByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsUserHasAccessToMessageAttachment(Guid attachmentId, Guid userId, CancellationToken cancellationToken);
}