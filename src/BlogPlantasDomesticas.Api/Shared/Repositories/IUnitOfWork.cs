namespace BlogPlantasDomesticas.Api.Shared.Repositories;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    
    Task<int> CommitAsync(CancellationToken cancellationToken);
}