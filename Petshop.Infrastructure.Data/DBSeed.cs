using System.Xml.Serialization;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Petshop.Core.Entity;
using TodoApi.Models;

namespace Petshop.Infrastructure.Data
{
    public class DBSeed
    {
        public static void SeedDB(PetshopContex ctx)
        {
            string password = "1234";
            byte[] passwordHashJoe, passwordSaltJoe, passwordHashAnn, passwordSaltAnn;
            CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);
            CreatePasswordHash(password, out passwordHashAnn, out passwordSaltAnn);
            
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();                   
            var cust1 = ctx.Pets.Add(new Pet()
            {
                Color = "Grey",
                Name = "Cipko",
                Type = "Dog",
                Price = 50
            });
            var user1 = ctx.User.Add(new User()
            {              
                    Username = "UserJoe",
                    PasswordHash = passwordHashJoe,
                    PasswordSalt = passwordSaltJoe,
                    IsAdmin = false              
            });
            var user2 = ctx.User.Add(
                new User
                {
                    Username = "AdminAnn",
                    PasswordHash = passwordHashAnn,
                    PasswordSalt = passwordSaltAnn,
                    IsAdmin = true
                });
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}