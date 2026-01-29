using BlogPlantasDomesticas.Api.Auth;
using BlogPlantasDomesticas.Api.Repositories;
using BlogPlantasDomesticas.Api.Services;
using BlogPlantasDomesticas.Api.Shared;
using BlogPlantasDomesticas.Api.Shared.Auth;
using BlogPlantasDomesticas.Api.Shared.Repositories;

namespace BlogPlantasDomesticas.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        services.AddScoped<ISeedService, SeedService>();

        return services;
    }
    
}