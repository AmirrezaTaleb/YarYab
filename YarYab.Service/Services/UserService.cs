using AutoMapper;
using YarYab.API.DTO;
using YarYab.Common;
using YarYab.Common.Exceptions;
using YarYab.Common.Helper;
using YarYab.Common.Utilities;
using YarYab.Data.Interfaces;
using YarYab.Domain.Models;
using YarYab.Service.DTO;
using YarYab.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YarYab.Data.Repositories;
using System.Threading;

namespace YarYab.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMemoryCache _cache;


        public UserService(IMemoryCache cache, IRepositoryManager repositoryManager, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task AddProfilePhotoAsync(int userId, IFormFile file, CancellationToken cancellationToken)
        {
            User user = await GetUserByIdAsync(userId, cancellationToken);

            user.ProfilePhoto = await file.ConvertToByteArrayAsync();

            await _repositoryManager.UserRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task AddUserSimpleAsync(AddSimpleUserDTO user, CancellationToken cancellationToken)
        {
            User userModel = user.ToEntity(_mapper);
            await _repositoryManager.UserRepository.AddAsync(userModel, cancellationToken);
        }

        public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            User user = await GetUserByIdAsync(userId, cancellationToken);
            user.SoftDelete();
            await _repositoryManager.UserRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task<User> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await _repositoryManager.UserRepository.GetByIdAsync(cancellationToken, userId);
        }

        public async Task SetLocation(SetUserLocationDTO user, CancellationToken cancellationToken)
        {
            User usermodel = await GetUserByIdAsync(user.Id, cancellationToken);
            usermodel.Latitude = user.Latitude;
            usermodel.Longitude = user.Longitude;
            await _repositoryManager.UserRepository.UpdateAsync(usermodel, cancellationToken);
        }

        public async Task UpdateUserAsync(EditUserDTO user, CancellationToken cancellationToken)
        {
            User userModel = user.ToEntity(_mapper);
            await _repositoryManager.UserRepository.UpdateAsync(userModel, cancellationToken);
        }
    }
}

