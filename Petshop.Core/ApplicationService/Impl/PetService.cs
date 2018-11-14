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
        readonly IOwnerRepository _ownerRepository;
        
        public PetService(IPetRepository petRepository,IOwnerRepository ownerRepository)
        {
            _perRepository = petRepository;
            _ownerRepository = ownerRepository;
        }

        public Pet GetPetInstance()
        {
            return new Pet();
        }

        public Pet AddPet(Pet newPet)
        {
            _perRepository.AddPet(newPet);
            return newPet;
        }

        public List<Pet> GetPets(Filter filter)
        {
            var petsList = _perRepository.ReadPets(filter).ToList();
            return petsList;
        }

        public Pet DeletePet(int selectedId)
        {
           return _perRepository.RemovePet(selectedId);
        }

        public Pet FindPetById(int Id)
        {
            Pet selectedPet =_perRepository.GetPetById(Id);         
            return selectedPet;
        }

        public Pet UpdatePet(Pet selectedPet)
        {
            return _perRepository.UpdatePet(selectedPet);
        }   
    }
}
