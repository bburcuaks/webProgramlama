using Hospital.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {

        private readonly DatabaseContext _databaseContext;

        public AdminApiController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }



        [Authorize(Roles = "admin")]
        [HttpGet]
        public IEnumerable<User> GetUserList()
        {
            var users = _databaseContext.Users.ToList();
            return users;
        }

        // GET api/<AdminApiController>/5
        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _databaseContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // POST api/<AdminApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AdminApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
