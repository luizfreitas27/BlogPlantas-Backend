using BlogPlantasDomesticas.Api.Contexts;
using BlogPlantasDomesticas.Api.Shared.Repositories;

namespace BlogPlantasDomesticas.Api.Repositories;

public class UnitOfWork : IUnitOfWork , IAsyncDisposable
{
    private AppDbContext _context;
    private IUserRepository? _userRepository; 
    private IRoleRepository? _roleRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public IUserRepository Users => 
        _userRepository ??= new UserRepository(_context);

    public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_context);


    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}