using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class OwnerRepository: IRepository<Owner>
    {
        private readonly PetshopContex _ctx;

        public OwnerRepository(PetshopContex ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Owner> GetAll(Filter filter)
        {
            if (filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                return _ctx.Owners
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage)
                    .OrderBy(p => p.ID);				
            }
            return _ctx.Owners;
        }

        public Owner Get(int id)
        {
            return _ctx.Owners.Include(c => c.Pet).FirstOrDefault(o => o.ID == id);
        }

        public void Add(Owner newModel)
        {
            _ctx.Attach(newModel).State = EntityState.Added;
            _ctx.SaveChanges();
        }

        public void Edit(Owner modelUpdate)
        {
            _ctx.Attach(modelUpdate).State = EntityState.Modified;
            _ctx.Entry(modelUpdate).Reference(o => o.Pet).IsModified = true;
            _ctx.SaveChanges();
        }

        public void Remove(int id)
        {
            var owner = _ctx.Remove(new Owner{ID = id}).Entity;
            _ctx.SaveChanges();
        }
    }
}