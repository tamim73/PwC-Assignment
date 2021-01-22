using API.Context;
using Lib.Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Stories
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {

        private AssignmentDbContext dbContext;

        public StoriesController(AssignmentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<StoriesController>
        [HttpGet]
        public Task<ActionResult> SearchStories()
        {
            return null;
        }

        // GET api/<StoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StoriesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
