namespace OnVibeAPI.Requests.Reaction;

public class UpsertReactionRequest
{
    public Guid MessageId { get; set; }
    public string? Emoji { get; set; }
}