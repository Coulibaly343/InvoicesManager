using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Interfaces.Repositories;
using InvoicesManager.Core.Users.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InvoicesManager.Core.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByEmailAsync(request.Email.ToLowerInvariant()) != null)
                return null;

            var user = new User(
                request.Name,
                request.Surname,
                request.Address,
                request.Email,
                request.Password);

            await _userRepository.AddAsync(user);

            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Email = user.Email,
            };
        }

    }
}
