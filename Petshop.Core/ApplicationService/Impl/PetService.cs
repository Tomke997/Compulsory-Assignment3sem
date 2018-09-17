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

        public List<Pet> GetPets()
        {
            var petsList = _perRepository.ReadPets().ToList();
            return petsList;
        }

        public List<Pet> GetPetsByType(string type)
        {
            List<Pet> listWithType = new List<Pet>();
            foreach (Pet pet in _perRepository.ReadPets().ToList())
            {
                if (pet.Type.ToUpper() == type.ToUpper())
                {
                    listWithType.Add(pet);
                }               
            }
            if(listWithType.Count==0)
            {
                return null;
            }
            return listWithType;
        }

        public Pet DeletePet(int selectedId)
        {
           return _perRepository.RemovePet(selectedId);
        }

        public Pet FindPetById(int Id)
        {
            Pet selectedPet =_perRepository.GetPetById(Id);
            selectedPet.PreviousOwner = _ownerRepository.ReadOwners().Where(owner => owner.PetsID == selectedPet.ID).ToList();
            return selectedPet;
        }

        public Pet UpdatePet(Pet selectedPet)
        {
            return _perRepository.UpdatePet(selectedPet);
        }

        public List<Pet> SortPetByPrice(List<Pet> petList)
        {
            return petList.OrderBy(pet => pet.Price).ToList();
            
        }

        public List<Pet> GetSelectedAmountOfPets(List<Pet> petList, int amount)
        {
           int i = 0;
           List<Pet> listWithPets = new List<Pet>();
           while(i<petList.Count() && i<amount)
            {
                listWithPets.Add(petList[i++]);
            }
            return listWithPets;
        }    
    }
}
