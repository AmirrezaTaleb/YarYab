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

        public async Task AddUserAsync(User user, CancellationToken cancellationToken)
        {
            await _repositoryManager.UserRepository.AddAsync(user, cancellationToken, true);
        }

        public Task DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

