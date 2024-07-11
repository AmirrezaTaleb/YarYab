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
        public async Task<ActionResult<int>> AddUser([FromBody] User user, CancellationToken cancellationToken)
        {
            await _userService.AddUserAsync(user, cancellationToken);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User user)
        {
            if (userId != user.Id)
            {
                return BadRequest("User ID mismatch");
            }

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
