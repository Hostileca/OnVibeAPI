namespace Application.Dtos.Chat;

public class ChatReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<Guid> MembersIds { get; set; }
}