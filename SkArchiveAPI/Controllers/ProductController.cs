using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkArchiveAPI.DTOs;
using SkArchiveDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkArchiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SkArchiveDbContext _db;

        public ProductController(SkArchiveDbContext db)
        {
            _db = db;
        }

        // GET: api/<ProductController>
        [HttpGet]
        [Authorize]
        public IEnumerable<Product> Get()
        {
            return _db.Products.ToList();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
