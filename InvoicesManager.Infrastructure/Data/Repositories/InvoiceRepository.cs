using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicesManager.Infrastructure.Data.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(InvoicesManagerContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Invoice>> GetAllWithProductsByUserIdAsync(int userId)
        => await _context.Invoices.Where(x => x.UserId == userId).Include(x => x.Products).ToListAsync();

        public async Task<Invoice> GetWithProductsByIdAsync(int id)
        => await _context.Invoices.Include(x => x.Products).SingleOrDefaultAsync(x => x.Id == id);
    }
}
