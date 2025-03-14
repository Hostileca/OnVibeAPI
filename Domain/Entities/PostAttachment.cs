namespace Domain.Entities;

public class PostAttachment : Attachment
{
    public Post Post { get; set; }
    public Guid PostId { get; set; }
}