using Microsoft.EntityFrameworkCore;
using Petshop.Core.Entity;
using TodoApi.Models;

namespace Petshop.Infrastructure.Data
{
    public class PetshopContex: DbContext
    {
        public PetshopContex(DbContextOptions<PetshopContex> opt) : base(opt)
        {}
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().HasOne(o => o.Pet).WithMany(p => p.PreviousOwner).OnDelete(DeleteBehavior.SetNull);
           // base.OnModelCreating(modelBuilder);
        }
    }
}