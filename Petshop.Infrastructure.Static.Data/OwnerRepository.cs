using System.Collections.Generic;
using System.Linq;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Data
{
    public class OwnerRepository: IOwnerRepository
    {
        static int staticId = 2;
        
        public Owner GetOwnerById(int id)
        {
            return FakeDB.ownerList
                .FirstOrDefault(owner => owner.ID == id);
        }

        public IEnumerable<Owner> ReadOwners()
        {
            return FakeDB.ownerList;
        }

        public Owner AddOwner(Owner newOwner)
        {
            newOwner.ID = ++staticId;
            var owners = FakeDB.ownerList.ToList();
            owners.Add(newOwner);
            FakeDB.ownerList = owners;
            FakeDB.ownerList.ToList().Add(newOwner);
            return newOwner;
        }

        public Owner RemoveOwner(int selectedId)
        {
            Owner founOwner = this.GetOwnerById(selectedId);
            if(founOwner!=null)
            {
                var owners = FakeDB.ownerList.ToList();
                owners.Remove(founOwner);
                FakeDB.ownerList = owners;
                return founOwner;
            }
            return null;
        }

        public Owner UpdateOwner(Owner updatedOwner)
        {
            Owner selectedOwner = this.GetOwnerById(updatedOwner.ID);
            if (selectedOwner != null)
            {
                selectedOwner.Address = updatedOwner.Address;
                selectedOwner.FirstName = updatedOwner.FirstName;
                selectedOwner.LastName = updatedOwner.LastName;
                selectedOwner.PhoneNumber = updatedOwner.PhoneNumber;
                return selectedOwner;
            }
            return null;
        }
    }
}