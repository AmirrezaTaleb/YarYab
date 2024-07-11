
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
    public interface IRequestService : IBaseService
    {
        Task<int> SendRequestAsync(Request request);
        Task<Request> GetRequestByIdAsync(int requestId);
        Task UpdateRequestAsync(Request request);
        Task DeleteRequestAsync(int requestId);
    }
}
