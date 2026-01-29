using BlogPlantasDomesticas.Api.Models;

namespace BlogPlantasDomesticas.Api.Shared.Repositories;

public interface IUserRepository
{
    void Add(User user);
    void Update(User user);
    void Remove(User user);

    Task<PagedList<User>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<bool> AdminExistsAsync(CancellationToken cancellationToken);
}