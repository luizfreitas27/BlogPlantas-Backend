using BlogPlantasDomesticas.Api.Settings;

namespace BlogPlantasDomesticas.Api.Configurations;

public static class SeedConfiguration
{
    public static IServiceCollection AddSeedConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SeedSettings>(configuration.GetSection("Seed"));

        return services;
    }
    
    
}