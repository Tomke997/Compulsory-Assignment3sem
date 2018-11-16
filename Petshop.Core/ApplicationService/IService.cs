using System.Collections.Generic;
using Petshop.Core.Entity;

namespace Petshop.Core.ApplicationService
{
    public interface IService<T>
    {
        void Create(T newModel);

        List<T> GetAll(Filter filter);

        void Update(T modelUpdate);

        T GetById(int id);

        void Delete(int id);
        
    }
}