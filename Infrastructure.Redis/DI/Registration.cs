using Contracts.Redis.Repositories;
using Infrastructure.Redis.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Infrastructure.Redis.DI;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("RedisConnection");
        var redis = ConnectionMultiplexer.Connect(redisConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(redis);
        services.AddScoped<IConnectionRepository, ConnectionRepository>();
        
        return services;
    }
}