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

namespace YarYab.API.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/requests")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> SendRequest([FromBody] SendRequestDTO request, CancellationToken cancellationToken)
        {
            await _requestService.SendRequestAsync(request, cancellationToken);
            return Ok();
        }

        [HttpGet("{requestId}")]
        public async Task<ActionResult<Request>> GetRequest(int requestId, CancellationToken cancellationToken)
        {
            var request = await _requestService.GetRequestByIdAsync(requestId, cancellationToken);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }
        [HttpDelete("{requestId}")]
        public async Task<IActionResult> DeleteRequest(int requestId, CancellationToken cancellationToken)
        {
            await _requestService.DeleteRequestAsync(requestId, cancellationToken);
            return NoContent();
        }
    }
}
