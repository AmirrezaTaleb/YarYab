using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Data.Interfaces
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IRequestRepository RequestRepository { get; }
        ICityRepository CityRepository { get; }
    }
}
