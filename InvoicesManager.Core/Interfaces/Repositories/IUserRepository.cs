using InvoicesManager.Core.Entities;
using System.Threading.Tasks;

namespace InvoicesManager.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
