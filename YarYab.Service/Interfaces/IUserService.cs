﻿using YarYab.Domain.Models;
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
        Task SetLocation(SetUserLocationDTO user, CancellationToken cancellationToken);
        Task AddProfilePhotoAsync(int userId,IFormFile file, CancellationToken cancellationToken);
        Task UpdateUserAsync(EditUserDTO user , CancellationToken cancellationToken);
        Task DeleteUserAsync(int userId, CancellationToken cancellationToken);
        Task<bool> CheckUserAreValidAsync(int receiverId, CancellationToken cancellationToken);
        Task<List<User>> GetUserByFilterAsync(GetAllUserSelectDTO? filter, CancellationToken cancellationToken);
        List<User> GetNearUser(List<User> users, GetNearUserSelectDTO? filter, CancellationToken cancellationToken);
        Task UpdateUserScoreAsync(UserScoreSelectDTO user, CancellationToken cancellationToken);
        Task<List<City>> GetCitiesByParentAsync(GetCitiesSelectDTO? filter, CancellationToken cancellationToken);
        Task<List<GetContactDTO>> GetAllMyFriendAsync(int userId, CancellationToken cancellationToken);
    }
}
