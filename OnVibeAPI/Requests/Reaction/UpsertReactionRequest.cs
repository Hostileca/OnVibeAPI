namespace OnVibeAPI.Requests.Reaction;

public class UpsertReactionRequest
{
    public Guid ChatId { get; set; }
    public Guid MessageId { get; set; }
    public string? Emoji { get; set; }
}