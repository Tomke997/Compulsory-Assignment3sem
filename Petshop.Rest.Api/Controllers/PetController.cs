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
        private readonly IService<Pet> _petService;

        public PetsController(IService<Pet> petService)
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
                return _petService.GetAll(filter);
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
            
            Pet selectedPet = _petService.GetById(id);
            
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
            _petService.Create(newPet);
            return newPet;
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
            _petService.Update(selectedPet);

            return selectedPet;
        }

        // DELETE api/pets/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            _petService.Delete(id);
            if (_petService.GetById(id) == null)
            {
                return BadRequest("Pet with this Id does not exist");
            }
            return null;
        }
    }
}