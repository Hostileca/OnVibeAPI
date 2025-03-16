using Application.Dtos.ChatMember;

namespace Application.Dtos.Chat;

public class ChatReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<ChatMemberReadDto> Members { get; set; }
}