using Application.Dtos.Attachment;
using Application.Enums;
using MediatR;

namespace Application.UseCases.Attachment.Queries;

public sealed class GetAttachmentByIdQuery : IRequest<AttachmentReadDto>
{
    public Guid AttachmentId { get; init; }
    public Guid InitiatorId { get; init; }
    public AttachmentType Type { get; init; }
}