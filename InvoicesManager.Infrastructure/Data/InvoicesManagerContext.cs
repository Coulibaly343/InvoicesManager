using InvoicesManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoicesManager.Infrastructure.Data
{
    public class InvoicesManagerContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }

        public InvoicesManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
