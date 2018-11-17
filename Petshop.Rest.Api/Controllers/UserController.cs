using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using TodoApi.Models;

namespace Petshop.Rest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpPost]
        public ActionResult<User> Post([FromBody]LoginInputModel model)
        {         
            var user = _userRepository.GetAll(null).FirstOrDefault(u => u.Username == model.Username);

            if (user != null)
                return BadRequest();
            
            byte[] passwordHashnewUser, passwordSaltnewUser;
            CreatePasswordHash(model.Password, out passwordHashnewUser , out passwordSaltnewUser );

            var newUser = new User
            {
                Username = model.Username,
                PasswordHash = passwordHashnewUser,
                PasswordSalt = passwordSaltnewUser,
                IsAdmin = false
            };
            _userRepository.Add(newUser);
                       
            return newUser;
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