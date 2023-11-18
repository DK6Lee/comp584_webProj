using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkArchiveDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkArchiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        private readonly SkArchiveDbContext _db;

        public BrandsController(SkArchiveDbContext db)
        {
            _db = db;
        }
        // GET: api/<BrandsController>
        [HttpGet]
        [Authorize]
        public IEnumerable<Brand> Get()
        {
            return _db.Brands.ToList();
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BrandsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
