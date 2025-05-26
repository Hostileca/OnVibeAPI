namespace Contracts.DataAccess.Models.Include;

public class MessageIncludes
{
    public bool IncludeReactions { get; init; }
    public bool IncludeSender { get; init; }
    public bool IncludeNotifications { get; init; }
}