using Application.Dtos.Attachment;
using Application.Enums;
using Contracts.DataAccess.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Attachment.Queries;

public class GetAttachmentByIdQueryHandler(
    IAttachmentRepository attachmentRepository)
    : IRequestHandler<GetAttachmentByIdQuery, AttachmentReadDto>
{
    public async Task<AttachmentReadDto> Handle(GetAttachmentByIdQuery request, CancellationToken cancellationToken)
    {
        var attachment = await GetAttachment(request, cancellationToken);

        if (attachment is null)
        {
            throw new NotFoundException(typeof(AttachmentBase), request.AttachmentId.ToString());
        }

        return attachment.Adapt<AttachmentReadDto>();
    }

    private async Task<AttachmentBase?> GetAttachment(GetAttachmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        switch (request.Type)
        {
            case AttachmentType.Message:
                return await GetMessageAttachment(request, cancellationToken);
            case AttachmentType.Post:
                return await attachmentRepository.GetPostAttachmentByIdAsync(request.AttachmentId, cancellationToken);
        }

        return null;
    }

    private async Task<MessageAttachment?> GetMessageAttachment(GetAttachmentByIdQuery request, CancellationToken cancellationToken)
    {
        var isUserHasAccess = await attachmentRepository.IsUserHasAccessToMessageAttachment(request.AttachmentId, request.InitiatorId, cancellationToken);

        if (!isUserHasAccess)
        {
            throw new ForbiddenException("You don't have permission to this attachment");
        }

        return await attachmentRepository.GetMessageAttachmentByIdAsync(request.AttachmentId, cancellationToken);
    }
}