using Contracts.Redis.Repositories;
using StackExchange.Redis;

namespace Infrastructure.Redis.Repositories;

public class ConnectionRepository(IConnectionMultiplexer redis) : IConnectionRepository
{
    private readonly IDatabase redisDatabase = redis.GetDatabase();
    private static readonly string _prefix = "userConnection";
    
    private static string Key(Guid userId) => $"{_prefix}_{userId}";
    
    public async Task AddConnectionAsync(Guid userId, string connectionId)
    {
        await redisDatabase.SetAddAsync(Key(userId), connectionId);
    }

    public async Task RemoveConnectionAsync(Guid userId, string connectionId)
    {
        await redisDatabase.SetRemoveAsync(Key(userId), connectionId);
    }

    public async Task<IEnumerable<string>> GetConnectionsAsync(Guid userId)
    {
        var members = await redisDatabase.SetMembersAsync(Key(userId));
        return members.Select(m => (string)m);
    }

    public async Task RemoveAllConnectionsAsync(Guid userId)
    {
        await redisDatabase.KeyDeleteAsync(Key(userId));
    }
}