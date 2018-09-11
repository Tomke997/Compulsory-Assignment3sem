using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            return _petService.GetPets();
        }

        // GET api/pets/5
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
           
            if (id < 1 || id != selectedPet.ID)
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