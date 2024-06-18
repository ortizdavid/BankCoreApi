using BankCoreApi.Models;

namespace BankCoreApi.Repositories
{   
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task CreateBatchAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(int limit, int offset);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByUniqueIdAsync(Guid uniqueId);
        Task<bool> ExistsRecordAsync(string? field, string? value);
    }
}