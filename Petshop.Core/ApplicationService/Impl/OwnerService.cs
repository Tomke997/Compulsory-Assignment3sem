using System.Collections.Generic;
using System.Linq;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;

namespace Petshop.Core.ApplicationService.Impl
{
    public class OwnerService: IService<Owner>
    {
        readonly IRepository<Owner> _ownerRepository;
        
        public OwnerService(IRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public void Create(Owner newObject)
        {
            _ownerRepository.Add(newObject);
        }

        public List<Owner> GetAll(Filter filter)
        {
            return _ownerRepository.GetAll(filter).ToList();
        }

        public void Update(Owner objectUpdate)
        {
            _ownerRepository.Edit(objectUpdate);
        }

        public Owner GetById(int id)
        {
            return _ownerRepository.Get(id);
        }

        public void Delete(int id)
        {
            _ownerRepository.Remove(id);
        }
    }
}