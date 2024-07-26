
using YarYab.Domain;
using YarYab.Domain.Models;
using YarYab.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Service
{
    public interface IContactService : IBaseService
    {
        Task AddContactAsync(AddContactDTO contact, CancellationToken cancellationToken);
        Task<Contact> GetContactByIdAsync(int contactId, CancellationToken cancellationToken);
        Task DeleteContactAsync(int contactId, CancellationToken cancellationToken);
        Task<bool> CheckAlreadyFriend(int userId, int friendId, CancellationToken cancellationToken);
    }
}
