using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
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
            //return _ctx.Owners.FirstOrDefault(o => o.ID == id);
            return _ctx.Owners.Include(c => c.Pet).FirstOrDefault(o => o.ID == id);
        }

        public IEnumerable<Owner> ReadOwners()
        {
            return _ctx.Owners;
        }

        public Owner AddOwner(Owner newOwner)
        {
            /*if (newOwner.Pet != null &&
                _ctx.ChangeTracker.Entries<Pet>().FirstOrDefault(ce => ce.Entity.ID == newOwner.Pet.ID) == null) ;
            {
                _ctx.Attach(newOwner.Pet);
            }            
            _ctx.Owners.Add(newOwner);
            _ctx.SaveChanges();*/
            _ctx.Attach(newOwner).State = EntityState.Added;
            _ctx.SaveChanges();
            return newOwner;
        }

        public Owner RemoveOwner(int selectedId)
        {
            var owner = _ctx.Remove(new Owner() {ID = selectedId}).Entity;
            /*var owner = _ctx.Owners.FirstOrDefault(c => c.ID == selectedId);
            _ctx.Owners.Remove(owner);*/
            _ctx.SaveChanges();
            return owner;
        }

        public Owner UpdateOwner(Owner updatedOwner)
        {
            /*if (updatedOwner.Pet != null &&
                _ctx.ChangeTracker.Entries<Pet>().FirstOrDefault(ce => ce.Entity.ID == updatedOwner.Pet.ID) == null) 
            {
                _ctx.Attach(updatedOwner.Pet);
            }
            else
            {
                _ctx.Entry(updatedOwner).Reference(o => o.Pet).IsModified = true;
            }
            _ctx.Owners.Update(updatedOwner);
            _ctx.SaveChanges();*/
            _ctx.Attach(updatedOwner).State = EntityState.Modified;
            _ctx.Entry(updatedOwner).Reference(o => o.Pet).IsModified = true;
            _ctx.SaveChanges();
            return updatedOwner;
            
            
        }
    }
}