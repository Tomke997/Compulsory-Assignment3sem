using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.ApplicationService;
using Petshop.Core.Entity;

namespace Petshop.Rest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }
        
        // GET api/pets
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get([FromQuery] Filter filter)
        {          
            try
            {
                return _petService.GetPets(filter);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/pets/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater then 0");
            }
            
            Pet selectedPet = _petService.FindPetById(id);
            
            if (selectedPet == null)
            {
                return BadRequest("Pet with this Id does not exist");
            }
            
            return selectedPet;
        }

        // POST api/pets
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet newPet)
        {
            return _petService.AddPet(newPet);
        }

        // PUT api/pets/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet selectedPet)
        {
           
            if (id < 1 || id != selectedPet.ID)
            {
                return BadRequest("Parameter Id and order ID must be the same");
            }
            _petService.UpdatePet(selectedPet);

            return selectedPet;
        }

        // DELETE api/pets/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            Pet selectedPet = _petService.DeletePet(id);
            if (selectedPet == null)
            {
                return BadRequest("Pet with this Id does not exist");
            }
            return null;
        }
    }
}