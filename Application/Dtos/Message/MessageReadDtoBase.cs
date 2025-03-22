namespace Application.Dtos.Message;

public abstract class MessageReadDtoBase
{
    public Guid? SenderId { get; set; }
    public Guid ChatId { get; set; }
}