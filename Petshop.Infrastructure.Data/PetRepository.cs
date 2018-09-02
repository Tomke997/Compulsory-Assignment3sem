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

        public Pet GetPetById(int selectedId)
        {
            foreach (Pet pet in FakeDB.petList.ToList())
            {
                if (pet.ID == selectedId)
                {
                    return pet;
                }
            }
            return null;
        }

        public IEnumerable<Pet> ReadPets()
        {
            return FakeDB.petList;
        }

        public Pet RemovePet(int selectedId)
        {
            Pet foundPet = this.GetPetById(selectedId);
            if(foundPet!=null)
            {
                var pets = FakeDB.petList.ToList();
                pets.Remove(foundPet);
                FakeDB.petList = pets;
                return foundPet;
            }
            return null;
        }

        public Pet UpdatePet(Pet updatedPet)
        {
            Pet foundPet = this.GetPetById(updatedPet.ID);
            if (foundPet != null)
            {
                foundPet.Name = updatedPet.Name;
                foundPet.Type = updatedPet.Type;
                foundPet.Birthdate = updatedPet.Birthdate;
                foundPet.SoldDate = updatedPet.SoldDate;
                foundPet.Color = updatedPet.Color;
                foundPet.PreviousOwner = updatedPet.PreviousOwner;
                foundPet.Price = updatedPet.Price;
                return foundPet;
            }
            return null;
        }
    }
}
