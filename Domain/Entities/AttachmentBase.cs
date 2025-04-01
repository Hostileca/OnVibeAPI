namespace Domain.Entities;

public abstract class AttachmentBase
{
    public Guid Id { get; set; }
    public string Data { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}