using Petshop.Core.Entity;
using System;
using System.Collections.Generic;

namespace Petshop.Core.ApplicationService
{
    public interface IPetService
    {
        List<Pet> GetPets();

         Pet CreatePet(
         string Name,
         string Type,
         DateTime Birthdate,
         DateTime SoldDate,
         string Color,
         string PreviousOwner,
         double Price);

        Pet AddPet(Pet newPet);
    }
}
