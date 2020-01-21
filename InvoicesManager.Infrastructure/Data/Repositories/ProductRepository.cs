using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Interfaces.Repositories;

namespace InvoicesManager.Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(InvoicesManagerContext context) : base(context)
        {
        }

    }
}
