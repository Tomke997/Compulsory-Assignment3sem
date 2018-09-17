using System.Collections.Generic;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        private readonly PetshopContex _ctx;

        public OwnerRepository(PetshopContex ctx)
        {
            _ctx = ctx;
        }
        
        public Owner GetOwnerById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Owner> ReadOwners()
        {
            throw new System.NotImplementedException();
        }

        public Owner AddOwner(Owner newOwner)
        {
            throw new System.NotImplementedException();
        }

        public Owner RemoveOwner(int selectedId)
        {
            throw new System.NotImplementedException();
        }

        public Owner UpdateOwner(Owner updatedOwner)
        {
            throw new System.NotImplementedException();
        }
    }
}