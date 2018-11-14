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
            return _ctx.Owners.Include(c => c.Pet).FirstOrDefault(o => o.ID == id);
        }

        public IEnumerable<Owner> ReadOwners(Filter filter)
        {
            if (filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                return _ctx.Owners
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage)
                    .OrderBy(p => p.ID);				
            }
            return _ctx.Owners;
        }
        
        public Owner AddOwner(Owner newOwner)
        {
            _ctx.Attach(newOwner).State = EntityState.Added;
            _ctx.SaveChanges();
            return newOwner;
        }

        public Owner RemoveOwner(int selectedId)
        {
            var owner = _ctx.Remove(new Owner() {ID = selectedId}).Entity;
            _ctx.SaveChanges();
            return owner;
        }

        public Owner UpdateOwner(Owner updatedOwner)
        {
            _ctx.Attach(updatedOwner).State = EntityState.Modified;
            _ctx.Entry(updatedOwner).Reference(o => o.Pet).IsModified = true;
            _ctx.SaveChanges();
            return updatedOwner;
            
            
        }
    }
}