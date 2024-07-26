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
    [Route("api/Contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;

        public ContactController(IContactService ContactService, IUserService userService)
        {
            _contactService = ContactService;
            _userService = userService;
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult<int>> AddContact([FromBody] AddContactDTO contact, CancellationToken cancellationToken)
        {
            if (await _userService.GetUserByIdAsync(contact.UserId, cancellationToken) != null && await _userService.GetUserByIdAsync(contact.FriendId, cancellationToken) != null)
            {
                if (!await _contactService.CheckAlreadyFriend(contact.UserId, contact.FriendId, cancellationToken))
                {
                    await _contactService.AddContactAsync(contact, cancellationToken);
                    return Ok();
                }
                else
                    return BadRequest("Already Friend !!");
            }
            else
                return NotFound();
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<Contact>> GetContact(int contactId, CancellationToken cancellationToken)
        {
            var Contact = await _contactService.GetContactByIdAsync(contactId, cancellationToken);
            if (Contact == null)
            {
                return NotFound();
            }
            return Ok(Contact);
        }
        [HttpDelete("[Action]")]
        public async Task<IActionResult> DeleteContact(int ContactId, CancellationToken cancellationToken)
        {
            await _contactService.DeleteContactAsync(ContactId, cancellationToken);
            return NoContent();
        }
    }
}
