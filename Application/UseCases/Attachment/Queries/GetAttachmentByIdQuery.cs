using Application.Dtos.Attachment;
using Application.Enums;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Attachment.Queries;

public sealed class GetAttachmentByIdQuery : RequestBase<AttachmentReadDto>
{
    public Guid AttachmentId { get; init; }
    public AttachmentType Type { get; init; }
}