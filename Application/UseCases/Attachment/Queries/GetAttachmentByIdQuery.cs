using Application.Dtos.Attachment;
using Application.Enums;
using MediatR;

namespace Application.UseCases.Attachment.Queries;

public sealed record GetAttachmentByIdQuery(Guid AttachmentId, Guid UserId, AttachmentType Type) : IRequest<AttachmentReadDto>;