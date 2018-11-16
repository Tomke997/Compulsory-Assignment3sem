using System;
using System.Collections.Generic;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using System.Linq;

namespace Petshop.Core.ApplicationService.Impl
{
    public class PetService : IService<Pet>
    {
        readonly IRepository<Pet> _petRepository;
        
        public PetService(IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
        }

        public void Create(Pet newObject)
        {
            _petRepository.Add(newObject);
        }

        public List<Pet> GetAll(Filter filter)
        {
            return _petRepository.GetAll(filter).ToList();
        }

        public void Update(Pet objectUpdate)
        {
             _petRepository.Edit(objectUpdate);
        }

        public Pet GetById(int id)
        {
            return _petRepository.Get(id);         
        }

        public void Delete(int id)
        {
             _petRepository.Remove(id);
        }
    }
}
