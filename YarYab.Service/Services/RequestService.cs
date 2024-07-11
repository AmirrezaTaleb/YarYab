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
    public class RequestService :  IRequestService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public RequestService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public Task DeleteRequestAsync(int requestId)
        {
            throw new NotImplementedException();
        }

        public Task<Request> GetRequestByIdAsync(int requestId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SendRequestAsync(Request request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRequestAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
