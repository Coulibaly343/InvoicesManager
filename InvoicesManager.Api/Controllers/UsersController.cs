using InvoicesManager.Core.Users.Commands.CreateUser;
using InvoicesManager.Core.Users.Commands.LoginUser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoicesManager.Api.Controllers
{
    public class UsersController : ApiBaseController
    {
        /// <summary>
        /// Register User
        /// </summary>
        /// <returns>Created at action result</returns>
        /// <response code="201">User has been created successfully.</response>
        /// <response code="400">User already exisits.</response>
        /// <response code="500">User creation failed.</response>
        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegisterUser([FromBody]CreateUserCommand command)
        {
            var user = await Mediator.Send(command);

            if (user is null) return BadRequest();

            return Created(nameof(RegisterUser), user);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <returns>UserDto </returns>
        /// <response code="201">User has been returned successfully.</response>
        /// <response code="400">User does not exists or password is incorrect.</response>
        /// <response code="500">Obtaining user failed.</response>
        [HttpPost("login")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand command)
        {
            var user = await Mediator.Send(command);
            if (user != null)
                return Ok(user);

            return BadRequest();
        }

    }
}

