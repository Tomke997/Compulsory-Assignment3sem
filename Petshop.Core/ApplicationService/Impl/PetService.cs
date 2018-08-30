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

        public Pet AddPet(Pet newPet)
        {
            _perRepository.AddPet(newPet);
            return newPet;
        }

        public Pet CreatePet(string Name, string Type, DateTime Birthdate, DateTime SoldDate, string Color, string PreviousOwner, double Price)
        {
            return new Pet()
            {
                Name = Name,
                Type = Type,
                Birthdate = Birthdate,
                SoldDate = SoldDate,
                Color = Color,
                PreviousOwner = PreviousOwner,
                Price = Price
            };
        }

        public List<Pet> GetPets()
        {
            return _perRepository.ReadPets().ToList();
        }
    }
}
