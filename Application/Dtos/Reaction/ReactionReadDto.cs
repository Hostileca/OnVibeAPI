namespace Application.Dtos.Reaction;

public class ReactionReadDto
{
    public Guid Id { get; set; }
    public string Emoji { get; set; }
    public Guid SenderId { get; set; }
}