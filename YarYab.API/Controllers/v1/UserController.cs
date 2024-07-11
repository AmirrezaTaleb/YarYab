using YarYab.API.DTO;
using YarYab.Common.Exceptions;
using YarYab.Common.Helper;
using YarYab.Data.Interfaces;
using YarYab.Domain.Models;
using YarYab.Service;
using YarYab.Service.DTO;
using YarYab.WebFramework.Api;
using YarYab.WebFramework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YarYab.Service.Services;
using YarYab.Service.Interfaces;

namespace YarYab.API.Controllers.v1
{
    [ApiController]
    [Route("api/users")]
    [ApiVersion("1")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddUser([FromBody] AddSimpleUserDTO user, CancellationToken cancellationToken)
        {
            await _userService.AddUserSimpleAsync(user, cancellationToken);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserDTO user, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserAsync(user, cancellationToken);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId, CancellationToken cancellationToken)
        {
            await _userService.DeleteUserAsync(userId, cancellationToken);
            return Ok();
        }
    }
}
