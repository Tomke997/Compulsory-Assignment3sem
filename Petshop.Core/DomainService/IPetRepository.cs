using Petshop.Core.Entity;
using System.Collections.Generic;

namespace Petshop.Core.DomainService
{
    public interface IPetRepository
    {
        IEnumerable<Pet> ReadPets(Filter filter);

        Pet AddPet(Pet newPet);

        Pet RemovePet(int selectedId);

        Pet GetPetById(int selectedId);

        Pet UpdatePet(Pet updatedPet);
    }
}
