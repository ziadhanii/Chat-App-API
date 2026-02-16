namespace ChatApp.Application.Contract;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Message> Messages { get; }
    IGenericRepository<BlockedUser> BlockedUsers { get; }
    IGenericRepository<AppUser> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
