using Petshop.Core.Entity;
using System;
using System.Collections.Generic;

namespace Petshop.Core.ApplicationService
{
    public interface IPetService
    {
        Pet GetPetInstance();

        List<Pet> GetPets(Filter filter);

        Pet AddPet(Pet newPet);

        Pet DeletePet(int selectedId);

        Pet UpdatePet(Pet selectedPet);

        Pet FindPetById(int Id);
    }
}
