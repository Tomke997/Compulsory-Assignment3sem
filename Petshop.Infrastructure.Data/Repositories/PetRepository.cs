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
        
        public IEnumerable<Pet> ReadPets()
        {
            return _ctx.Pets;
        }

        public Pet AddPet(Pet newPet)
        {
            var pet = _ctx.Pets.Add(newPet).Entity;
            _ctx.SaveChanges();
            return pet;
        }

        public Pet RemovePet(int selectedId)
        {
            var pet = _ctx.Remove(new Pet() {ID = selectedId}).Entity;
            /*var pet = _ctx.Pets.FirstOrDefault(c => c.ID == selectedId);
            _ctx.Pets.Remove(pet);*/
            _ctx.SaveChanges();
            return pet;
        }

        public Pet GetPetById(int selectedId)
        {
            //selectedPet.PreviousOwner = _ownerRepository.ReadOwners().Where(owner => owner.PetsID == selectedPet.ID).ToList();          
            return _ctx.Pets.Include(c => c.PreviousOwner).FirstOrDefault(c => c.ID == selectedId);
        }

        public Pet UpdatePet(Pet updatedPet)
        {
            /*_ctx.Attach(updatedPet).State = EntityState.Modified;
            _ctx.SaveChanges();
            return updatedPet;*/
            
            _ctx.Attach(updatedPet).State = EntityState.Modified;
            _ctx.Entry(updatedPet).Collection(c => c.PreviousOwner).IsModified = true;
            var owners = _ctx.Owners.Where(o => o.Pet.ID == updatedPet.ID
                                                && !updatedPet.PreviousOwner.Exists(co => co.ID == o.ID));
            foreach (var owner in owners)
            {
                owner.Pet = null;
                _ctx.Entry(owner).Reference(o => o.Pet)
                    .IsModified = true;
            }
            _ctx.SaveChanges();
            return updatedPet;
        }
    }
}