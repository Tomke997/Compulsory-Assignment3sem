using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.ApplicationService;
using Petshop.Core.Entity;
using Petshop.Infrastructure.Data.Repositories;

namespace Petshop.Rest.Api.Controllers
{
    public class UserController
    {
        [Route("api/[user]")]
        [ApiController]
        public class PetsController : ControllerBase
        {
            private readonly IPetService _petService;

            public PetsController(IPetService petService)
            {
                _petService = petService;
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
            
            // GET api/pets
            [HttpGet]
            public ActionResult<IEnumerable<Pet>> Get()
            {
                return null;
            }

            // GET api/users/5
            [HttpGet("{id}")]
            public ActionResult<Pet> Get(int id)
            {
                Pet selectedPet = _petService.FindPetById(id);
                if (selectedPet == null)
                {
                    return BadRequest("Pet with this Id does not exist");
                }
                return selectedPet;
            }

            // POST api/pets
            [HttpPost]
            public ActionResult<Pet> Post([FromBody] Pet newPet)
            {
                return _petService.AddPet(newPet);
            }

            // PUT api/pets/5
            [HttpPut("{id}")]
            public ActionResult<Pet> Put(int id, [FromBody] Pet selectedPet)
            {
           
                if (id < 1)
                {
                    return BadRequest("Parameter Id and order ID must be the same");
                }
                else if (_petService.FindPetById(id) == null)
                {
                    return BadRequest("Owner with this id does not exist"); 
                }
                _petService.UpdatePet(selectedPet);
                return Ok($"Owner with Id: {id} was updated");
            }

            // DELETE api/pets/5
            [HttpDelete("{id}")]
            public ActionResult<Pet> Delete(int id)
            {
                Pet selectedPet = _petService.DeletePet(id);
                if (selectedPet == null)
                {
                    return BadRequest("Pet with this Id does not exist");
                }
                return Ok($"Pet with Id: {id} was Deleted");
            }
        }
    }
}