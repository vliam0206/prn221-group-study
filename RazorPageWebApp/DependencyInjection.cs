using Application.IServices;
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
        services.AddSession(options
            => options.IdleTimeout = TimeSpan.FromMinutes(60));        

        services.AddScoped<IClaimService, ClaimService>();

        // Add app middlewares
        services.AddSingleton<CheckAuthenticationMiddleware>();

        return services;
    }
}
