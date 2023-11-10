using Dapper.Database.Model;
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
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repo;
        public BookController(IBookRepository repo)
        {
            _repo = repo;
        }

        //___________ GetAll _________
        [HttpGet]
        //public async Task<IEnumerable<Person>> Get()
        public async Task<IActionResult> Get()
        {
            var list = await _repo.Get();
            //return persons;
            return Ok(list);
        }

        //___________ Get _________
        [HttpGet("Id")]
        //public async Task<Person> Get(int id)
        public async Task<IActionResult> Get(Guid id)
        {
            var persons = await _repo.GetById(id);
            if (persons == null)
            {
                return NotFound();
            }
            //return persons;
            return Ok(persons);
        }
        
        //___________ Insert _________
        [HttpPost]
        public async Task<IActionResult> Post(Book dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var result = await _repo.Add(dto);
            if (!result)
                return BadRequest("Insert Faild");

            return Ok();
        }

        //___________ Update _________
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] Book dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var obj = _repo.GetById(id);

            if (obj == null)
                return NotFound();

            dto.Id = id;

            var result = await _repo.Update(dto);
            if (!result)
                return BadRequest("update Faild");

            return Ok();
        }

        //___________ Insert buld _________
        [HttpPost("bulk")]
        public async Task<IActionResult> AddBulkBooks(IEnumerable<Book> books)
        {
            try
            {
                // Call the AddBooks method in the repository
                await _repo.AddList(books);
                return Ok("Books added successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        //___________ Delete _________
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var person = _repo.GetById(id);
            if (person == null)
                return NotFound();

            var result = await _repo.Delete(id);
            if (!result)
                return BadRequest("Faild to delete");

            return Ok();
        }
    }
}
