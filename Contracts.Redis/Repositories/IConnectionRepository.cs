namespace Contracts.Redis.Repositories;

public interface IConnectionRepository
{
    Task AddConnectionAsync(Guid userId, string connectionId);
    Task RemoveConnectionAsync(Guid userId, string connectionId);
    Task<IEnumerable<string>> GetConnectionsAsync(Guid userId);
    Task RemoveAllConnectionsAsync(Guid userId);
}