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

        public async Task DeleteRequestAsync(int requestId, CancellationToken cancellationToken)
        {
            Request request = await GetRequestByIdAsync(requestId, cancellationToken);
            request.SoftDelete();
            await _repositoryManager.RequestRepository.UpdateAsync(request, cancellationToken);
        }

        public async  Task<Request> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken)
        {
            return await _repositoryManager.RequestRepository.GetByIdAsync(cancellationToken, requestId);
        }

        public  async Task SendRequestAsync(SendRequestDTO request , CancellationToken cancellationToken)
        {
            Request requestModel = request.ToEntity(_mapper);
            requestModel.Status = RequestStatus.NotSeen;
            await _repositoryManager.RequestRepository.AddAsync(requestModel, cancellationToken);
        }

        public async Task UpdateRequestAsync(Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
