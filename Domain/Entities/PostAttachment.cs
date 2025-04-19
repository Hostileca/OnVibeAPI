namespace Domain.Entities;

public class PostAttachment : AttachmentBase
{
    public Post Post { get; set; }
    public Guid PostId { get; set; }
}