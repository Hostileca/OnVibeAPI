using System.Reflection;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class Registration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.ServicesConfigure();
        services.MediatorConfigure();
        services.AddMapster();

        return services;
    }

    private static void ServicesConfigure(this IServiceCollection services)
    {
        services.AddScoped<IHashingService, HashingService>();
        services.AddScoped<ITokenService, TokenService>();
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

    // private static IServiceCollection ValidationConfigure(this IServiceCollection services)
    // {
    //     services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    //     services.AddFluentValidationAutoValidation();
    //     
    //     return services;
    // }
}