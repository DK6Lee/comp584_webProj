using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SkArchiveAPI.DTOs;
using SkArchiveDB;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

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

        // LINQ statement to get brand info including product category
        [HttpGet("BrandProductCategory/{name}/{category}")]
        public BrandProductCategory? GetBrandCategoryCount(string name, string category)
        {
            /*
             * SELECT ID, BRANDNAME, CATEGORY, COUNT(Product.CATEGORY)
             * FROM Brands
             * WHERE brand.ID = ID AND product.CATEGORY = category
             */
            return (from brand in _db.Brands
                    where brand.Name == name
                    select new BrandProductCategory()
                    {
                        Id = brand.Id,
                        BrandName = brand.Name,
                        Category = category,
                        ProdCatCount = brand.Products.Count(p => p.Category == category && p.BrandId == brand.Id)
                    }).SingleOrDefault();
        }

        // LINQ statement to get brand info including country
        /*
        [HttpGet("BrandCountry/{country}")]
        public BrandCountry? GetBrandByCountry(string country)
        {
            return (from brand in _db.Brands
                    where brand.Country == country
                    select new BrandCountry()
                    {
                        Iso2 = brand.Iso2,
                        Iso3 = brand.Iso3,
                        Country = brand.Country,
                        BrandName = brand.Name
                    }).SingleOrDefault();
        }
        */

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
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
