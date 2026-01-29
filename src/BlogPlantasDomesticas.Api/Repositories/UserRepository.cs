using BlogPlantasDomesticas.Api.Contexts;
using BlogPlantasDomesticas.Api.Enums;
using BlogPlantasDomesticas.Api.Helpers;
using BlogPlantasDomesticas.Api.Models;
using BlogPlantasDomesticas.Api.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogPlantasDomesticas.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public void Update(User user)
    {
        _context.Entry(user).Property(u => u.RefreshToken).IsModified = true;
        _context.Entry(user).Property(u => u.ExpiresAt).IsModified = true;
        _context.Entry(user).Property(u => u.UpdatedAt).IsModified = true;
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task<PagedList<User>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = _context.Users
            .Include(u => u.Role);
        
        return await PaginationHelper.CreateAsync(query, pageNumber, pageSize, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Where(u => u.RefreshToken == refreshToken)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync(cancellationToken);
        
    }

    public async Task<bool> AdminExistsAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AnyAsync(u => u.RoleId == (int)RoleType.Admin, cancellationToken);
    }
}