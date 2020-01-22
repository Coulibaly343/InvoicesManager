using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Exceptions;
using InvoicesManager.Core.Interfaces.Repositories;
using InvoicesManager.Core.Users.Models;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace InvoicesManager.Core.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is null
                || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Email = user.Email,
            };
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        //private async Task<string> GenerateToken(User account, IJWTSettings jwtSettings)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[] {
        //        new Claim (ClaimTypes.NameIdentifier, account.Id.ToString ()),
        //        new Claim (ClaimTypes.Name, account.Email),
        //        new Claim (ClaimTypes.Role, account.Role)
        //        }),
        //        Issuer = "",
        //        Expires = DateTime.Now.AddDays(jwtSettings.ExpiryDays),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //        SecurityAlgorithms.HmacSha512Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return await Task.FromResult(tokenHandler.WriteToken(token));
        //}

    }
}
