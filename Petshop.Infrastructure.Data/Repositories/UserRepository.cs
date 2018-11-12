using System.Collections.Generic;
using TodoApi.Models;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class UserRepository
    {
        private readonly PetshopContex _ctx;

        public UserRepository(PetshopContex ctx)
        {
            _ctx = ctx;
        }
        
        public IEnumerable<User> ReadPets()
        {
            return _ctx.User;
        }
    }
}