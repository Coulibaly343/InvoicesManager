using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Interfaces.Repositories;

namespace InvoicesManager.Infrastructure.Data.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(InvoicesManagerContext context) : base(context)
        {
        }
    }
}
