using System.Collections.Generic;
using Petshop.Core.Entity;

namespace Petshop.Core.ApplicationService
{
    public interface IOwnerService
    {
        
        Owner CreateOwner(Owner newOwner);

        List<Owner> GetAllOwners();

        Owner UpdateOwner(Owner owner);

        Owner GetOwnerById(int id);

        Owner DeleteOwner(int ownerId);
    }
}