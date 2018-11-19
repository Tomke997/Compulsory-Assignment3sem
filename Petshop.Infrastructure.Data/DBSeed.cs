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
            byte[] passwordHashJoe, passwordSaltJoe;
            CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);
          
                User userJoe = new User {
                    Username = "UserJoe",
                    PasswordHash = passwordHashJoe,
                    PasswordSalt = passwordSaltJoe,
                    IsAdmin = true
                };
            
            ctx.Users.Add(userJoe);
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