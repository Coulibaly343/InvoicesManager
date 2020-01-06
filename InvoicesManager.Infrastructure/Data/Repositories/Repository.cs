using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoicesManager.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly InvoicesManagerContext _context;

        protected Repository(InvoicesManagerContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<T>> GetAllAsync(bool isTracking)
            => await _context.Set<T>().ToListAsync();

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
