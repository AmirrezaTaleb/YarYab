﻿using YarYab.Common;
using YarYab.Data.Interfaces;
using YarYab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(YarYabContext dbContext)
            : base(dbContext)
        {
        }
    }
}
