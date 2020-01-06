using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoicesManager.Core.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(bool isTracking);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
