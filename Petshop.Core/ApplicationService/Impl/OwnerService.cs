using System.Collections.Generic;
using System.Linq;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;

namespace Petshop.Core.ApplicationService.Impl
{
    public class OwnerService: IOwnerService
    {
        readonly IOwnerRepository _ownerRepository;
        
        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        
        public Owner CreateOwner(Owner newOwner)
        {
           return _ownerRepository.AddOwner(newOwner);
        }

        public List<Owner> GetAllOwners(Filter filter)
        {
            return _ownerRepository.ReadOwners(filter).ToList();
        }

        public Owner UpdateOwner(Owner owner)
        {
            return _ownerRepository.UpdateOwner(owner);
        }

        public Owner GetOwnerById(int id)
        {
            return _ownerRepository.GetOwnerById(id);
        }

        public Owner DeleteOwner(int id)
        {
            return _ownerRepository.RemoveOwner(id);
        }
    }
}