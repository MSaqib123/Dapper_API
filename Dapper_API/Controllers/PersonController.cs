using Dapper.Database.Models;
using Dapper.Database.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Dapper_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repo;
        public PersonController(IPersonRepository repo)
        {
            _repo = repo;
        }

        //___________ GetAll _________
        [HttpGet]
        //public async Task<IEnumerable<Person>> Get()
        public async Task<IActionResult> Get()
        {
            var persons = await _repo.GetPersons();
            //return persons;
            return Ok(persons);
        }

        //___________ Get _________
        [HttpGet("Id")]
        //public async Task<Person> Get(int id)
        public async Task<IActionResult> Get(int id)
        {
            var persons = await _repo.GetPersonById(id);
            if (persons == null)
            {
                return NotFound();
            }
            //return persons;
            return Ok(persons);
        }
        
        //___________ Insert _________
        [HttpPost]
        public async Task<IActionResult> Post(Person dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var result = await _repo.AddPerson(dto);
            if (!result)
                return BadRequest("Insert Faild");

            return Ok();
        }

        //___________ Insert _________
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] Person dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var person = _repo.GetPersonById(id);

            if (person == null)
                return NotFound();

            dto.Id = id;

            var result = await _repo.UpdatePerson(dto);
            if (!result)
                return BadRequest("update Faild");

            return Ok();
        }


        //___________ Delete _________
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var person = _repo.GetPersonById(id);
            if (person == null)
                return NotFound();

            var result = await _repo.DeletePerson(id);
            if (!result)
                return BadRequest("Faild to delete");

            return Ok();
        }
    }
}
