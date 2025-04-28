using System.Reflection;
using Application.Dtos.Chat;
using Application.Dtos.Like;
using Application.Dtos.Message;
using Application.Dtos.Post;
using Application.Dtos.User;
using Application.Services.Implementations;
using Application.Services.Implementations.ExtraLoaders;
using Application.Services.Implementations.Notification;
using Application.Services.Interfaces;
using Application.Services.Interfaces.Notification;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class Registration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.ServicesConfigure();
        services.AddExtraLoaders();
        services.MediatorConfigure();
        services.AddMapster();

        return services;
    }

    private static void ServicesConfigure(this IServiceCollection services)
    {
        services.AddScoped<IHashingService, HashingService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserContext, UserContext>();
        
        services.AddScoped<IMessageNotificationService, MessageNotificationService>();
    }
    
    private static void MediatorConfigure(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
    
    private static void AddMapster(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
    }

    private static void AddExtraLoaders(this IServiceCollection services)
    {
        services.AddScoped<IExtraLoader<UserReadDto>, UserReadDtoExtraLoader>();
        services.AddScoped<IExtraLoader<MessageReadDto>, MessageReadDtoExtraLoader>();
        services.AddScoped<IExtraLoader<PostReadDto>, PostReadDtoExtraLoader>();
        services.AddScoped<IExtraLoader<ChatReadDto>, ChatReadDtoExtraLoader>();
        services.AddScoped<IExtraLoader<LikeReadDto>, LikeReadDtoExtraLoader>();
    }
}