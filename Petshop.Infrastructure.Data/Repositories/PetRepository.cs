using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class PetRepository: IPetRepository
    {
        private readonly PetshopContex _ctx;

        public PetRepository(PetshopContex ctx)
        {
            _ctx = ctx;
        }
        
        public IEnumerable<Pet> ReadPets(Filter filter)
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

        public Pet AddPet(Pet newPet)
        {         
            _ctx.Attach(newPet).State = EntityState.Added;
            _ctx.Entry(newPet).Collection(o => o.PreviousOwner).IsModified = true;
            _ctx.SaveChanges();
            
            return newPet;
        }

        public Pet RemovePet(int selectedId)
        {
            var pet = _ctx.Remove(new Pet() {ID = selectedId}).Entity;
            _ctx.SaveChanges();
            return pet;
        }

        public Pet GetPetById(int selectedId)
        {                    
            return _ctx.Pets.Include(c => c.PreviousOwner).FirstOrDefault(c => c.ID == selectedId);
        }

        public Pet UpdatePet(Pet updatedPet)
        {
            _ctx.Update(updatedPet).State = EntityState.Modified;
            _ctx.Entry(updatedPet).Collection(o => o.PreviousOwner).IsModified = true;
            var owners = _ctx.Owners.Where(o =>
                o.Pet.ID == updatedPet.ID && !updatedPet.PreviousOwner.Exists(co => co.ID == o.ID));
            foreach (var owner in owners)
            {
                owner.Pet = null;
                _ctx.Entry(owner).Reference(o => o.Pet).IsModified = true;
            }
            _ctx.SaveChanges();

            return updatedPet;
            
        }
    }
}