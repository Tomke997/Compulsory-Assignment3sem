using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.Entity;

namespace PetshopApp.Rest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<List<Pet>> Get()
        {


            var pet1 = new Pet()
            {
                ID = 3,
                Name = "Shrek",
                Type = "Ogre",
                Birthdate = DateTime.Parse("07 / 09 / 2001"),
                SoldDate = DateTime.Parse("08 / 07 / 2010"),
                Color = "Green",
                PreviousOwner = "Andrew Adamson",
                Price = 99.99
            };
            var pet2 = new Pet()
            {
                ID = 2,
                Name = "Dog",
                Type = "Cat",
                Birthdate = DateTime.Parse("12 / 08 / 1410"),
                SoldDate = DateTime.Parse("28 / 11 / 1544"),
                Color = "Pink",
                PreviousOwner = "Fabio Monaco",
                Price = 105.50
            };
            return new List<Pet>() { pet1, pet2 };

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody] Pet pet)
        {
          return pet.Name;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}