using YarYab.Common;
using YarYab.Data.Interfaces;
using YarYab.Domain;
using YarYab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Data.Repositories
{
    public class  RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(YarYabContext dbContext)
            : base(dbContext)
        {
        }
    }
}
