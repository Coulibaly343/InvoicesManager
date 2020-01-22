using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicesManager.Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(InvoicesManagerContext context) : base(context)
        {
        }

        public Task<User> GetByEmailAsync(string email)
        => _context.Users
                    .SingleOrDefaultAsync(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant());
    }
}
