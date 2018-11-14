using System.Collections.Generic;
using TodoApi.Models;

namespace Petshop.Core.DomainService
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        void Add(User newUser);
        void Edit(User selectedUser);
        void Remove(int id);
    }
}