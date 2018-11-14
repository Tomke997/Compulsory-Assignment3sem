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
        public ActionResult<IEnumerable<Owner>> Get([FromQuery] Filter filter)
        {
            return _ownerService.GetAllOwners(filter);
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
            if (id < 1)
            {
                return BadRequest("Parameter Id and order ID must be the same");
            }
            _ownerService.UpdateOwner(owner);
            return owner;
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
            return null;
        }
    }
}