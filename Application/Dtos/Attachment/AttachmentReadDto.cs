namespace Application.Dtos.Attachment;

public class AttachmentReadDto
{
    public Guid Id { get; set;}
    public string ContentType { get; set; }
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}