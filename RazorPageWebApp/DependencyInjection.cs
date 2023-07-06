using Application.IServices;
using AutoMapper;
using RazorPageWebApp.Middlewares;
using RazorPageWebApp.Services;

namespace RazorPageWebApp;

public static class DependencyInjection
{
    public static IServiceCollection AddWebAppServices (this IServiceCollection services)
    {        
        // Add httpcontext, cache memory for using session        
        services.AddHttpContextAccessor(); // Add DI for IHttpContextAccessor

        services.AddDistributedMemoryCache();
        services.AddSignalR();
        services.AddSession(options
            => options.IdleTimeout = TimeSpan.FromMinutes(60));        

        services.AddScoped<IClaimService, ClaimService>();
        services.AddAutoMapper(typeof(Mapper));

        // Add app middlewares
        services.AddSingleton<CheckAuthenticationMiddleware>();
        services.AddScoped<LoadNotificationMiddleware>();
        // Add singleton
        services.AddTransient<LiveChatHub>();

        return services;
    }
}
