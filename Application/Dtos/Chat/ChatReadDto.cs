using Application.Dtos.ChatMember;
using Application.Dtos.Message;

namespace Application.Dtos.Chat;

public class ChatReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int UnreadMessagesCount { get; set; }
    public MessageReadDto? Preview { get; set; }
    public IList<ChatMemberReadDto> Members { get; set; }
}