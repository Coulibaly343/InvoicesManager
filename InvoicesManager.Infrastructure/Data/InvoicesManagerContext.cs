using Microsoft.EntityFrameworkCore;

namespace InvoicesManager.Infrastructure.Data
{
    public class InvoicesManagerContext : DbContext
    {

        public InvoicesManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
