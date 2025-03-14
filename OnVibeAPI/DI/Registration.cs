using System.Reflection;
using System.Text;
using Mapster;
using MapsterMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace OnVibeAPI.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;

public static class Registration
{
    public static void AddPresentation(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.SwaggerConfigure();
        services.AuthorizationConfigure(configuration);
        //services.AddScoped<ExceptionHandlingMiddleware>();
        //services.AddScoped<LoggingMiddleware>();
    }
    
    private static void AuthorizationConfigure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["JwtSettings:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true
            };
            
            // options.Events = new JwtBearerEvents
            // {
            //     OnMessageReceived = context =>
            //     {
            //         var accessToken = context.Request.Query["access_token"];
            //
            //         var path = context.HttpContext.Request.Path;
            //         if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chats/hub"))
            //         {
            //             context.Token = accessToken;
            //         }
            //         return Task.CompletedTask;
            //     }
            // };
        });
        services.AddAuthorization(option =>
        {
            // option.AddPolicy(Policies.RequireStaff, 
            //     policy => policy.RequireRole("Admin"));
        });
    }

    private static void SwaggerConfigure(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
    
    public static void StartApplication(this WebApplication webApplication)
    {
        webApplication.UseSwagger();
        webApplication.UseSwaggerUI();
        //webApplication.MapHub<ChatHub>("/chats/hub");
        //webApplication.UseHangfireDashboard();
        webApplication.MapControllers();
        webApplication.UseHttpsRedirection();
        //webApplication.UseMiddleware<ExceptionHandlingMiddleware>();
        //webApplication.UseMiddleware<LoggingMiddleware>();
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();
        webApplication.Run();
    }
}