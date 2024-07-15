
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
        Task SendRequestAsync(SendRequestDTO request , CancellationToken cancellationToken);
        Task<Request> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken);
        Task UpdateRequestAsync(Request request, CancellationToken cancellationToken    );
        Task DeleteRequestAsync(int requestId, CancellationToken cancellationToken);
    }
}
