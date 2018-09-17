using Microsoft.EntityFrameworkCore;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Data
{
    public class PetshopContex: DbContext
    {
        public PetshopContex(DbContextOptions<PetshopContex> opt) : base(opt)
        {
            
        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}