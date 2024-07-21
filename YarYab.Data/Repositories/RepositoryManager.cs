using YarYab.Common;
using YarYab.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace YarYab.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager, IScopedDependency
    {
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IRequestRepository> _requestRepository;
        private readonly Lazy<ICityRepository> _cityRepository;
        public RepositoryManager(YarYabContext context)
        {
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _requestRepository = new Lazy<IRequestRepository>(() => new RequestRepository(context));
            _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(context));
        }
        public IUserRepository UserRepository => _userRepository.Value;
        public IRequestRepository RequestRepository => _requestRepository.Value;
        public ICityRepository CityRepository => _cityRepository.Value;
    }
}
