using InvoicesManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoicesManager.Infrastructure.Data
{
    public class InvoicesManagerContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public InvoicesManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(a => a.Invoices)
                .WithOne(b => b.CreatedBy)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Invoice>()
                .HasMany(a => a.Products)
                .WithOne(b => b.Invoice)
                .HasForeignKey(x => x.InvoiceId);

        }
    }
}
