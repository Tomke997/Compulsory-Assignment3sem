using Petshop.Core.Entity;
using System;
using System.Collections.Generic;

namespace Petshop.Core.ApplicationService
{
    public interface IPetService
    {
        List<Pet> GetPets();

        List<Pet> GetPetsByType(string type);

        Pet CreatePet(
         string Name,
         string Type,
         DateTime Birthdate,
         DateTime SoldDate,
         string Color,
         string PreviousOwner,
         double Price);

        Pet AddPet(Pet newPet);

        void ClearValidation();

       void PrintValidation(string alert, string text);

        Pet DeletePet(int selectedId);


    }
}
