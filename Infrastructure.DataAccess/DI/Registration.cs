using Contracts.DataAccess.Interfaces;
using DataAccess.Contexts;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DI;

public static class Registration
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
    }
    
    private static void AddDatabase(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetSection("DbConnection").Value;

        if (string.IsNullOrEmpty(dbConnectionString))
        {
            dbConnectionString = configuration.GetConnectionString("BaseDbConnection");
        }

        serviceCollection.AddDbContextFactory<BaseDbContext>(options => options.UseNpgsql(dbConnectionString), ServiceLifetime.Scoped);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
    }
}