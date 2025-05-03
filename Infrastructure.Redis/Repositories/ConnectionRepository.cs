using Contracts.Redis.Repositories;
using StackExchange.Redis;

namespace Infrastructure.Redis.Repositories;

public class ConnectionRepository(IConnectionMultiplexer redis) : IConnectionRepository
{
    private readonly IDatabase redisDatabase = redis.GetDatabase();
    private static readonly string _prefix = "userConnection";
    
    private static string Key(string userId) => $"{_prefix}_{userId}";
    
    public async Task AddConnectionAsync(string userId, string connectionId)
    {
        await redisDatabase.SetAddAsync(Key(userId), connectionId);
    }

    public async Task RemoveConnectionAsync(string userId, string connectionId)
    {
        await redisDatabase.SetRemoveAsync(Key(userId), connectionId);
    }

    public async Task<IEnumerable<string>> GetConnectionsAsync(string userId)
    {
        var members = await redisDatabase.SetMembersAsync(Key(userId));
        return members.Select(m => (string)m);
    }

    public async Task RemoveAllConnectionsAsync(string userId)
    {
        await redisDatabase.KeyDeleteAsync(Key(userId));
    }
}