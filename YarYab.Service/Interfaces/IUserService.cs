using YarYab.Domain.Models;
using YarYab.Service.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Service.Interfaces
{
    public interface IUserService: IBaseService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task AddUserAsync(User user, CancellationToken cancellationToken);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }
}
