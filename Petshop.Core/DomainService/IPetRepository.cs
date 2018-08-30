using Petshop.Core.Entity;
using System.Collections.Generic;

namespace Petshop.Core.DomainService
{
    public interface IPetRepository
    {
        IEnumerable<Pet> ReadPets();

        Pet AddPet(Pet newPet);
    }
}
