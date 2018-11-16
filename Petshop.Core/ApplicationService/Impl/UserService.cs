using System.Collections.Generic;
using System.Linq;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using TodoApi.Models;

namespace Petshop.Core.ApplicationService.Impl
{
    public class UserService: IService<User>
    {
        readonly IRepository<User> _userRepository;
        
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        public void Create(User newModel)
        {
            _userRepository.Add(newModel);
        }

        public List<User> GetAll(Filter filter)
        {
            return _userRepository.GetAll(filter).ToList();
        }

        public void Update(User modelUpdate)
        {
            _userRepository.Edit(modelUpdate);
        }

        public User GetById(int id)
        {
            return _userRepository.Get(id);
        }

        public void Delete(int id)
        {
            _userRepository.Remove(id);
        }
    }
}