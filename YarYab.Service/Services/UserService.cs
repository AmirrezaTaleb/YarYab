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

        public async Task<bool> CheckUserAreValidAsync(int userId, CancellationToken cancellationToken)
        {
            var User = await _repositoryManager.UserRepository.GetByIdAsync(cancellationToken, userId);
            return User != null;
        }

        public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            User user = await GetUserByIdAsync(userId, cancellationToken);
            user.SoftDelete();
            await _repositoryManager.UserRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task<List<GetContactDTO>> GetAllMyFriendAsync(int userId, CancellationToken cancellationToken)
        {
            var Dto = new List<GetContactDTO>();
            var contact = await _repositoryManager.ContactRepository.Get().Include(c => c.Friend).Where(c => c.UserId == userId).ToListAsync(cancellationToken);
            foreach (var item in contact)
            {
                var contactItem = new GetContactDTO();
                contactItem.Friend = item.Friend;
                contactItem.ContactId = item.Id;

                Dto.Add(contactItem);
            }
            return Dto;
        }

        public async Task<List<City>> GetCitiesByParentAsync(GetCitiesSelectDTO? filter, CancellationToken cancellationToken)
        {
            return await _repositoryManager.CityRepository.Get().Where(c => (c.ParentId ?? 0) == (filter.Parent_Id ?? 0)).ToListAsync();
        }

        public List<User> GetNearUser(List<User> users, GetNearUserSelectDTO? filter, CancellationToken cancellationToken)
        {
            if (filter?.Latitude != null && filter?.Longitude != null)
            {
                int radius = 10;
                users = users.Where(user => GeoHelper.Haversine((double)filter.Latitude, (double)filter.Longitude, user.Latitude, user.Longitude) <= radius).ToList();
            }
            return users;
        }

        public async Task<List<User>> GetUserByFilterAsync(GetAllUserSelectDTO? filter, CancellationToken cancellationToken)
        {
            var searchuser = _repositoryManager.UserRepository.Get();
            if (filter?.Age != null)
            {
                int AgeRange = 1;
                searchuser = searchuser.Where(a => a.Age > filter.Age - AgeRange && a.Age < filter.Age + AgeRange);
            }
            if (filter?.Gender != null)
            {
                searchuser = searchuser.Where(g => g.Gender == filter.Gender);
            }
            if (filter?.City_Id != null)
            {
                searchuser = searchuser.Where(c => c.CityId == filter.City_Id);

            }

            return await searchuser.ToListAsync(cancellationToken);
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

        public async Task UpdateUserScoreAsync(UserScoreSelectDTO user, CancellationToken cancellationToken)
        {
            User userModel = await GetUserByIdAsync(user.User_id, cancellationToken);
            userModel.Score = userModel.Score ?? 0 + user.Update_Score;
            await _repositoryManager.UserRepository.UpdateAsync(userModel, cancellationToken);
        }

     }
}

