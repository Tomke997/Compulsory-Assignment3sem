using System;
using System.Collections.Generic;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using System.Linq;

namespace Petshop.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {
        static int staticId = 3;

        public Pet AddPet(Pet newPet)
        {
            staticId++;
            newPet.ID = staticId;
            var pets = FakeDB.petList.ToList();
            pets.Add(newPet);
            FakeDB.petList = pets;
            return newPet;
        }

        public IEnumerable<Pet> ReadPets()
        {
            return FakeDB.petList;
        }
    }
}
