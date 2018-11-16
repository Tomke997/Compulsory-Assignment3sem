using System.Collections.Generic;
using Petshop.Core.Entity;

namespace Petshop.Core.DomainService
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(Filter filter);
        
        T Get(int id);
        
        void Add(T newModel);
        
        void Edit(T modelUpdate);
        
        void Remove(int id);
    }
}