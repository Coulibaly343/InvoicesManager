using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Users.Models;
using MediatR;

namespace InvoicesManager.Core.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
