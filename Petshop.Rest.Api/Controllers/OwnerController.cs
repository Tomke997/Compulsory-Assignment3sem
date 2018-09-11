using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.ApplicationService;
using Petshop.Core.Entity;

namespace Petshop.Rest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController: ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }
        
        // GET api/owners
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get()
        {
            return _ownerService.GetAllOwners();
        }

        // GET api/owners/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            Owner selectedOwner = _ownerService.GetOwnerById(id);
            if (selectedOwner == null)
            {
                return BadRequest("Owner with this Id does not exist");
            }
            return selectedOwner;
        }

        // POST api/owners
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner newOwner)
        {
            return _ownerService.CreateOwner(newOwner);
        }

        // PUT api/owners/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            if (id < 1 || id != owner.ID)
            {
                return BadRequest("Parameter Id and order ID must be the same");
            }
            else if (_ownerService.GetOwnerById(id) == null)
            {
                return BadRequest("Owner with this id does not exist"); 
            }

            _ownerService.UpdateOwner(owner);
            return Ok($"Owner with Id: {id} was updated");

        }

        // DELETE api/owners/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            Owner selectedOwner = _ownerService.DeleteOwner(id);
            if (selectedOwner == null)
            {
                return BadRequest("Owner with this Id does not exist");
            }
            return Ok($"Owner with Id: {id} was Deleted");
        }
    }
}