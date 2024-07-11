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
        public async Task<ActionResult<int>> SendRequest([FromBody] Request request)
        {
            var requestId = await _requestService.SendRequestAsync(request);
            return Ok(requestId);
        }

        [HttpGet("{requestId}")]
        public async Task<ActionResult<Request>> GetRequest(int requestId)
        {
            var request = await _requestService.GetRequestByIdAsync(requestId);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        [HttpPut("{requestId}")]
        public async Task<IActionResult> UpdateRequest(int requestId, [FromBody] Request request)
        {
            if (requestId != request.Id)
            {
                return BadRequest("Request ID mismatch");
            }

            await _requestService.UpdateRequestAsync(request);
            return NoContent();
        }

        [HttpDelete("{requestId}")]
        public async Task<IActionResult> DeleteRequest(int requestId)
        {
            await _requestService.DeleteRequestAsync(requestId);
            return NoContent();
        }
    }
}
