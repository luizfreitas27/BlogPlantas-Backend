namespace BlogPlantasDomesticas.Api.Shared;

public interface ISeedService
{
    Task SeedAdminAsync(CancellationToken cancellationToken =  default);
}