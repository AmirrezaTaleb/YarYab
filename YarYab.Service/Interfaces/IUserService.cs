using YarYab.Domain.Models;
using YarYab.Service.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YarYab.API.DTO;
using System.Threading;

namespace YarYab.Service.Interfaces
{
    public interface IUserService: IBaseService
    {
        Task<User> GetUserByIdAsync(int userId, CancellationToken cancellationToken);
        Task AddUserSimpleAsync(AddSimpleUserDTO user, CancellationToken cancellationToken);
        Task UpdateUserAsync(EditUserDTO user , CancellationToken cancellationToken);
        Task DeleteUserAsync(int userId, CancellationToken cancellationToken);
    }
}
