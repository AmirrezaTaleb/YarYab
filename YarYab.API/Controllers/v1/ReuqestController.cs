using YarYab.API.DTO;
using YarYab.Common.Exceptions;
using YarYab.Data.Interfaces;
using YarYab.Domain.Models;
using YarYab.Service;
using YarYab.Service.DTO;
using YarYab.WebFramework.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using YarYab.Service.Interfaces;

namespace YarYab.API.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/requests")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IUserService _userService;

        public RequestController(IRequestService requestService, IUserService userService)
        {
            _requestService = requestService;
            _userService = userService;
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult<int>> SendRequest([FromBody] SendRequestDTO request, CancellationToken cancellationToken)
        {
            if (await _userService.CheckUserAreValidAsync(request.SenderId, cancellationToken) && await _userService.CheckUserAreValidAsync(request.ReceiverId, cancellationToken))
            {
                await _requestService.SendRequestAsync(request, cancellationToken);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<Request>> GetRequest(int requestId, CancellationToken cancellationToken)
        {
            var request = await _requestService.GetRequestByIdAsync(requestId, cancellationToken);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }
        [HttpDelete("[Action]")]
        public async Task<IActionResult> DeleteRequest(int requestId, CancellationToken cancellationToken)
        {
            await _requestService.DeleteRequestAsync(requestId, cancellationToken);
            return NoContent();
        }
    }
}
