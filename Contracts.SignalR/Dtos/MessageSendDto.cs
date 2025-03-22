namespace Contracts.SignalR.Dtos;

public class MessageSendDto
{
    public Guid ChatId { get; set; }
    public Guid? SenderId { get; set; }
    public string Text { get; set; }
    public IList<Guid> AttachmentsIds { get; set; }
    public DateTime Date { get; set; }
}