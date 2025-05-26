using Application.Dtos.Reaction;

namespace Application.Dtos.Message;

public class MessageReadDto : MessageReadDtoBase
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; }
    public IList<Guid> AttachmentsIds { get; set; }
    public IList<ReactionReadDto> Reactions { get; set; }
    public IDictionary<Guid, bool> UserToRead { get; set; }
}