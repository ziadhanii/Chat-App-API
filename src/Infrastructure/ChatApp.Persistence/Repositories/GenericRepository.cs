namespace ChatApp.Persistence.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : class
{
    public async Task<IReadOnlyList<T>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.Set<T>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<T>()
            .FirstOrDefaultAsync(
                m => EF.Property<int>(m, "Id") == id,
                cancellationToken);
    }

    public async Task AddAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        context.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await context.Set<T>()
            .FirstOrDefaultAsync(
                m => EF.Property<int>(m, "Id") == id,
                cancellationToken);

        if (entity is not null)
        {
            context.Set<T>().Remove(entity);
        }
    }

    public async Task<bool> ExistAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<T>()
            .AnyAsync(
                m => EF.Property<int>(m, "Id") == id,
                cancellationToken);
    }
}