using Contracts.SignalR.NotificationServices;
using Infrastructure.SignalR.NotificationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SignalR.DI;

public static class Registration
{
    public static IServiceCollection AddInfrastructureSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        
        services.AddScoped<IMessageNotificationService, MessageNotificationService>();
        
        return services;
    }
}