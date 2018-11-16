using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class PetRepository: IRepository<Pet>
    {
        private readonly PetshopContex _ctx;

        public PetRepository(PetshopContex ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Pet> GetAll(Filter filter)
        {
            if (filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                return _ctx.Pets
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage)
                    .OrderBy(p => p.ID);				
            }
            return _ctx.Pets;
        }

        public Pet Get(int id)
        {
            return _ctx.Pets.Include(c => c.PreviousOwner).FirstOrDefault(c => c.ID == id);
        }

        public void Add(Pet newModel)
        {
            _ctx.Attach(newModel).State = EntityState.Added;
            _ctx.Entry(newModel).Collection(o => o.PreviousOwner).IsModified = true;
            _ctx.SaveChanges();
        }

        public void Edit(Pet modelUpdate)
        {
            _ctx.Update(modelUpdate).State = EntityState.Modified;
            _ctx.Entry(modelUpdate).Collection(o => o.PreviousOwner).IsModified = true;
            var owners = _ctx.Owners.Where(o =>
                o.Pet.ID == modelUpdate.ID && !modelUpdate.PreviousOwner.Exists(co => co.ID == o.ID));
            foreach (var owner in owners)
            {
                owner.Pet = null;
                _ctx.Entry(owner).Reference(o => o.Pet).IsModified = true;
            }
            _ctx.SaveChanges();
        }

        public void Remove(int id)
        {
            var pet = _ctx.Remove(new Pet() {ID = id}).Entity;
            _ctx.SaveChanges();
        }
    }
}