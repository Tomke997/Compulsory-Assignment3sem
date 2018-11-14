using System.Collections.Generic;
using Petshop.Core.DomainService;
using TodoApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PetshopContex _ctx;

        public UserRepository(PetshopContex ctx)
        {
            _ctx = ctx;
        }
        
        public IEnumerable<User> GetAll()
        {
            return _ctx.Users.ToList();
        }

        public User Get(int id)
        {
            return _ctx.Users.FirstOrDefault(b => b.Id == id);
        }

        public void Add(User entity)
        {
            _ctx.Users.Add(entity);
            _ctx.SaveChanges();
        }

        public void Edit(User entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Remove(int id)
        {
            var item = _ctx.Users.FirstOrDefault(b => b.Id == id);
            _ctx.Users.Remove(item);
            _ctx.SaveChanges();
        }
    }
}