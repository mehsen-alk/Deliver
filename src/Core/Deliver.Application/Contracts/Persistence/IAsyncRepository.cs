using Microsoft.EntityFrameworkCore.Storage;

namespace Deliver.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<IDbContextTransaction> BeginTransactionAsync();
}