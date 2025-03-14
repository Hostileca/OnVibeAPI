namespace Domain.Entities;

public class MessageAttachment : Attachment
{
    public Message Message { get; set; }
    public Guid MessageId { get; set; }
}