using System;
using System.Collections.Generic;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {      
        public IEnumerable<Pet> ReadPets()
        {           
            return FakeDB.petList;
        }
    }
}
