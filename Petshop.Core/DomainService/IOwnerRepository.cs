using System.Collections.Generic;
using Petshop.Core.Entity;

namespace Petshop.Core.DomainService
{
    public interface IOwnerRepository
    {
        Owner GetOwnerById(int id);
        
        IEnumerable<Owner> ReadOwners();

        Owner AddOwner(Owner newOwner);

        Owner RemoveOwner(int selectedId);

        Owner UpdateOwner(Owner updatedOwner);
    }
}