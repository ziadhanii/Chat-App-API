using ChatApp.Application.Interfaces;

namespace ChatApp.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IGenericRepository<Message> Messages { get; }
    public IGenericRepository<BlockedUser> BlockedUsers { get; }
    public IGenericRepository<AppUser> Users { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Messages = new GenericRepository<Message>(_context);
        BlockedUsers = new GenericRepository<BlockedUser>(_context);
        Users = new GenericRepository<AppUser>(_context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
