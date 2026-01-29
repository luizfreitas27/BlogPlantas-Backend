using BlogPlantasDomesticas.Api.Models;

namespace BlogPlantasDomesticas.Api.Shared.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetRoleById (int id, CancellationToken cancellationToken); 
}