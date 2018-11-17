using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;

namespace Petshop.Rest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController: ControllerBase
    {
        private readonly IRepository<Owner> _ownerService;

        public OwnersController(IRepository<Owner> ownerService)
        {
            _ownerService = ownerService;
        }
        
        // GET api/owners     
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_ownerService.GetAll(filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/owners/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            Owner selectedOwner = _ownerService.Get(id);
            if (selectedOwner == null)
            {
                return BadRequest("Owner with this Id does not exist");
            }
            return selectedOwner;
        }

        // POST api/owners
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner newOwner)
        {
            _ownerService.Add(newOwner);
            return newOwner;
        }

        // PUT api/owners/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            if (id < 1)
            {
                return BadRequest("Parameter Id and order ID must be the same");
            }
            _ownerService.Edit(owner);
            return owner;
        }

        // DELETE api/owners/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            _ownerService.Remove(id);
            if (_ownerService.Get(id) == null)
            {
                return BadRequest("Owner with this Id does not exist");
            }
            return null;
        }
    }
}