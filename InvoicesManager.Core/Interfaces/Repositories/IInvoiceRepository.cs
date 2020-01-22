using InvoicesManager.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoicesManager.Core.Interfaces.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<Invoice> GetWithProductsByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllWithProductsByUserIdAsync(int userId);
    }
}
