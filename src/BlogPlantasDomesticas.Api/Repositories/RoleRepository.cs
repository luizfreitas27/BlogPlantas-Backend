using BlogPlantasDomesticas.Api.Contexts;
using BlogPlantasDomesticas.Api.Models;
using BlogPlantasDomesticas.Api.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogPlantasDomesticas.Api.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetRoleById(int id, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}