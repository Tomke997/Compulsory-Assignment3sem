using System.Collections.Generic;
using System.Linq;
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
            return _ctx.Pets.FirstOrDefault(c => c.ID == selectedId);
        }

        public Pet GetPetById(int selectedId)
        {
            throw new System.NotImplementedException();
        }

        public Pet UpdatePet(Pet updatedPet)
        {
            throw new System.NotImplementedException();
        }
    }
}