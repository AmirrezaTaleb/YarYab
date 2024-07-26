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
using NLog.Filters;

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
        [HttpPost("[Action]")]
        public async Task<ActionResult<int>> AddUser([FromBody] AddSimpleUserDTO user, CancellationToken cancellationToken)
        {
            await _userService.AddUserSimpleAsync(user, cancellationToken);
            return Ok();
        }
        [HttpPatch("[Action]")]
        public async Task<ActionResult<int>> SetProfilePhoto(int userId, IFormFile file, CancellationToken cancellationToken)
        {
            await _userService.AddProfilePhotoAsync(userId, file, cancellationToken);
            return Ok();
        }
        [HttpPatch("[Action]")]
        public async Task<ActionResult<int>> SetLocation([FromBody] SetUserLocationDTO user, CancellationToken cancellationToken)
        {
            await _userService.SetLocation(user, cancellationToken);
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
        [HttpGet("[Action]")]
        public async Task<ActionResult<List<User>>> GetAllUser([FromQuery] GetAllUserSelectDTO? filter, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByFilterAsync(filter, cancellationToken);
            user = _userService.GetNearUser(user, filter.Location, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("[Action]")]
        public async Task<ActionResult<List<GetContactDTO>>> GetAllMyFriend(int userId, CancellationToken cancellationToken)
        {
            var Friends = await _userService.GetAllMyFriendAsync(userId, cancellationToken);
            if (Friends == null)
            {
                return NotFound();
            }
            return Ok(Friends);
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<List<City>>> GetCities([FromQuery] GetCitiesSelectDTO? filter, CancellationToken cancellationToken)
        {
            var Cities = await _userService.GetCitiesByParentAsync(filter, cancellationToken);


            if (Cities == null)
            {
                return NotFound();
            }
            return Ok(Cities);
        }
        [HttpPut("[Action]/{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserDTO user, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserAsync(user, cancellationToken);
            return Ok();
        }
        [HttpPut("[Action]/{userId}")]
        public async Task<IActionResult> UpdateUserScore([FromBody] UserScoreSelectDTO user, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserScoreAsync(user, cancellationToken);
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
