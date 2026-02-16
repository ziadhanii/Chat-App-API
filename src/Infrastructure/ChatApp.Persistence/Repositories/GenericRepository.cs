namespace ChatApp.Persistence.Repositories;

public class GenericRepository<T>(
    ApplicationDbContext context,
    IFileService fileService) : IGenericRepository<T>
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
        IFormFile? file = default,
        string? email = default,
        CancellationToken cancellationToken = default)
    {
        if (file is not null && file.Length > 0 && !string.IsNullOrWhiteSpace(email))
        {
            var filePath = await fileService.SaveImageAsync(file, email);

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                var imageProperty = typeof(T).GetProperty("ImageUrl");

                if (imageProperty != null && imageProperty.CanWrite)
                {
                    imageProperty.SetValue(entity, filePath);
                }
            }
        }

        await context.Set<T>().AddAsync(entity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        context.Set<T>().Update(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await context.Set<T>()
            .FirstOrDefaultAsync(
                m => EF.Property<int>(m, "Id") == id,
                cancellationToken);

        if (entity is null)
            return;

        context.Set<T>().Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
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