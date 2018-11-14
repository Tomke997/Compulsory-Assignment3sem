using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Petshop.Core.ApplicationService;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using Petshop.Infrastructure.Data.Repositories;
using TodoApi.Helpers;
using TodoApi.Models;

namespace Petshop.Rest.Api.Controllers
{
    public class TokensController: ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public TokensController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpPost]
        public IActionResult Login([FromBody]LoginInputModel model)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = GenerateToken(user)
            });
        }
        
        [HttpPost]
        public ActionResult<User> Post([FromBody]LoginInputModel model)
        {         
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Username == model.Username);
                       
            if (user != null)
             return Unauthorized();
            
            byte[] passwordHashnewUser, passwordSaltnewUser;
            CreatePasswordHash(model.Password, out passwordHashnewUser , out passwordSaltnewUser );

            var newUser = new User()
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
        
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
        
        private string GenerateToken(User user)
        {            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            if (user.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    JwtSecurityKey.Key, 
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null, // issuer - not needed (ValidateIssuer = false)
                    null, // audience - not needed (ValidateAudience = false)
                    claims.ToArray(), 
                    DateTime.Now,               // notBefore
                    DateTime.Now.AddMinutes(10)));  // expires

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }  
}