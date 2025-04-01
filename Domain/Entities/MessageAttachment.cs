namespace Domain.Entities;

public class MessageAttachment : AttachmentBase
{
    public Message Message { get; set; }
    public Guid MessageId { get; set; }
}