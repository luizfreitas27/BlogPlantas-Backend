using BlogPlantasDomesticas.Api.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BlogPlantasDomesticas.Api.Configurations;

public static class DbContextConfig
{
    public static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
    
}