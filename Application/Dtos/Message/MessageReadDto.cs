namespace Application.Dtos.Message;

public class MessageReadDto : MessageReadDtoBase
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public IList<Guid> AttachmentsIds { get; set; }
    public DateTime Date { get; set; }
}