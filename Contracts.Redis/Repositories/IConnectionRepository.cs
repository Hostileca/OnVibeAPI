namespace Contracts.Redis.Repositories;

public interface IConnectionRepository
{
    Task AddConnectionAsync(string userId, string connectionId);
    Task RemoveConnectionAsync(string userId, string connectionId);
    Task<IEnumerable<string>> GetConnectionsAsync(string userId);
    Task RemoveAllConnectionsAsync(string userId);
}