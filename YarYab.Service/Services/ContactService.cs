using AutoMapper;
using AutoMapper.QueryableExtensions;
using YarYab.API.DTO;
using YarYab.Common;
using YarYab.Common.Consts;
using YarYab.Common.Exceptions;
using YarYab.Data.Interfaces;
using YarYab.Domain;
using YarYab.Domain.Models;
using YarYab.Service.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Service
{
    public class ContactService : IContactService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public ContactService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task AddContactAsync(AddContactDTO contact, CancellationToken cancellationToken)
        {
            Contact contactModel = contact.ToEntity(_mapper);
            await _repositoryManager.ContactRepository.AddAsync(contactModel, cancellationToken);
        }

        public async Task DeleteContactAsync(int ContactId, CancellationToken cancellationToken)
        {
            Contact Contact = await GetContactByIdAsync(ContactId, cancellationToken);
            Contact.SoftDelete();
            await _repositoryManager.ContactRepository.UpdateAsync(Contact, cancellationToken);
        }


        public async Task<Contact> GetContactByIdAsync(int ContactId, CancellationToken cancellationToken)
        {
            return await _repositoryManager.ContactRepository.GetByIdAsync(cancellationToken, ContactId);
        }

        public async Task<bool> CheckAlreadyFriend(int userId, int friendId, CancellationToken cancellationToken)
        {
            if (await _repositoryManager.ContactRepository.Get().Where(c => c.UserId == userId && c.FriendId == friendId).AnyAsync(cancellationToken))
                return true;
            return false;
        }
    }
}
