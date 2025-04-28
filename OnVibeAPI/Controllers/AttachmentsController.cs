using Application.Enums;
using Application.UseCases.Attachment.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnVibeAPI.Controllers;

public class AttachmentsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAttachmentById(Guid id, AttachmentType type, CancellationToken cancellationToken)
    {
        var query = new GetAttachmentByIdQuery{ AttachmentId = id, InitiatorId = InitiatorId, Type = type};
        var attachment = await mediator.Send(query, cancellationToken);
        
        return File(attachment.Data, attachment.ContentType, attachment.FileName); 
    }
}