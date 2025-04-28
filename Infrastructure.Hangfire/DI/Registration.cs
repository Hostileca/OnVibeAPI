using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Hangfire.DI;

public static class Registration
{
    public static IServiceCollection AddInfrastructureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("HangfireConnection");
        services.AddHangfire(config => 
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(bo => 
                    bo.UseNpgsqlConnection(connection), new PostgreSqlStorageOptions()));
        
        services.AddHangfireServer();

        return services;
    }
}