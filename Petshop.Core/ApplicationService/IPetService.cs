using Petshop.Core.Entity;
using System;
using System.Collections.Generic;

namespace Petshop.Core.ApplicationService
{
    public interface IPetService
    {
        Pet GetPetInstance();

        List<Pet> GetPets();

        List<Pet> GetPetsByType(string type);

        Pet AddPet(Pet newPet);

        void ClearValidation();

        void PrintValidation(string alert, string text);

        Pet DeletePet(int selectedId);

        Pet UpdatePet(Pet selectedPet);

        Pet FindPetById(int Id);

        List<Pet> SortPetByPrice(List<Pet> petList);

        List<Pet> GetSelectedAmountOfPets(List<Pet> petList,int amount);
    }
}
