using Contracts.DataAccess.Interfaces;
using DataAccess.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class AttachmentRepository(BaseDbContext context) : IAttachmentRepository
{
    public async Task<IList<Guid>> GetAttachmentsIdsByPostIdAsync(Guid postId, CancellationToken cancellationToken)
    {
        return await context.PostAttachments
            .Where(x => x.PostId == postId)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Guid>> GetAttachmentsIdsByMessageIdAsync(Guid messageId, CancellationToken cancellationToken)
    {
        return await context.MessageAttachments
            .Where(x => x.MessageId == messageId)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<PostAttachment?> GetPostAttachmentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.PostAttachments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<MessageAttachment?> GetMessageAttachmentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.MessageAttachments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> IsUserHasAccessToMessageAttachment(Guid attachmentId, Guid userId, CancellationToken cancellationToken)
    {
        return await context.MessageAttachments.AnyAsync(a => a.Message.Chat.Members.Any(cm => cm.UserId == userId), cancellationToken);
    }
}