using Application.Dtos.User;

namespace Application.Dtos.Message;

public abstract class MessageReadDtoBase
{
    public UserShortReadDto? Sender { get; set; }
    public Guid ChatId { get; set; }
}