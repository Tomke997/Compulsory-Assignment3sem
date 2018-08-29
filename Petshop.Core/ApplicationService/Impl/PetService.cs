using System;
using System.Collections.Generic;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using System.Linq;

namespace Petshop.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        readonly IPetRepository _perRepository;
        public PetService(IPetRepository petRepository)
        {
            _perRepository = petRepository;
        }

        public List<Pet> GetPets()
        {
            return _perRepository.ReadPets().ToList();
        }
    }
}
