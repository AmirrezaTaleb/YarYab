using YarYab.Common;
using YarYab.Data.Interfaces;
using YarYab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Data.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(YarYabContext dbContext)
            : base(dbContext)
        {
        }
    }
}
