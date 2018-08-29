using Petshop.Core.Entity;
using System.Collections.Generic;

namespace Petshop.Core.ApplicationService
{
    public interface IPetService
    {
        List<Pet> GetPets();
    }
}
