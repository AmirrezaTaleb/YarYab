using YarYab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Service
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}
