using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.ApplicationService;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using TodoApi.Models;

namespace Petshop.Rest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IService<User> _userService;

        public UsersController(IService<User> userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public ActionResult<User> Post([FromBody]LoginInputModel model)
        {         
            var user = _userService.GetAll(null).FirstOrDefault(u => u.Username == model.Username);

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
            _userService.Create(newUser);
                       
            return newUser;
        }
        
        // DELETE api/pets/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            _userService.Delete(id);
            if (_userService.GetById(id) == null)
            {
                return BadRequest("User with this Id does not exist");
            }
            return null;
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get([FromQuery] Filter filter)
        {          
            try
            {
                return Ok(_userService.GetAll(filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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