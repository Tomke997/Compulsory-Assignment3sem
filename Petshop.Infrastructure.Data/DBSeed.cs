using System.Collections.Generic;
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
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();    
            
            string password = "1112";
            byte[] passwordHashJoe, passwordSaltJoe, passwordHashAnn, passwordSaltAnn;
            CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);

            List<User> users = new List<User>
            {
                new User {
                    Username = "UserJoe",
                    PasswordHash = passwordHashJoe,
                    PasswordSalt = passwordSaltJoe,
                    IsAdmin = true
                }};
            
            ctx.Users.AddRange(users);
            ctx.SaveChanges();                                 
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