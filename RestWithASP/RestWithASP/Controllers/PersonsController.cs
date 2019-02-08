using Microsoft.AspNetCore.Mvc;
using RestWithASP.Model;
using RestWithASP.Services;

namespace RestWithASP.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {

        private IPersonServices _personService;

        public PersonsController(IPersonServices personService)
        {
            _personService = personService;
        }
      
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Person value)
        {
            
            if (value == null) return BadRequest();

            return Ok(_personService.Create(value));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Person value)
        {
            if (value == null) return BadRequest();

            return Ok(_personService.Update(value));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
