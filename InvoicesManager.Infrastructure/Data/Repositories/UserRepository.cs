using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Interfaces.Repositories;

namespace InvoicesManager.Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(InvoicesManagerContext context) : base(context)
        {
        }
    }
}
