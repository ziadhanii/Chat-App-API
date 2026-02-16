using Microsoft.AspNetCore.Http;

namespace ChatApp.Application.Contract;

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        T entity,
        IFormFile? file = default,
        string? email = default,
        CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        T entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}