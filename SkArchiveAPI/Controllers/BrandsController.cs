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
        [HttpGet("BrandMoisturizerCount/{id}")]
        public BrandProductCategory? GetBrandMoisturizer(int id)
        {
            return (from brand in _db.Brands
                    where brand.Id == id
                    select new BrandProductCategory()
                    {
                        Id = brand.Id,
                        BrandName = brand.Name,
                        ProdCatCount = brand.Products.Count(p => p.Category == "Moisturizer" && p.BrandId == brand.Id)
                    }).SingleOrDefault();
        }

        [HttpGet("BrandCategoryCount/{id}")]
        public BrandCatCount? GetBrandCategoryCount(int id)
        {
            return (from brand in _db.Brands
                    where brand.Id == id
                    select new BrandCatCount()
                    {
                        Id = brand.Id,
                        BrandName = brand.Name,
                        CategoryCount = (from product in _db.Products
                                         where product.BrandId == id
                                         select product.Category
                                        ).Distinct().Count()
                    }).SingleOrDefault();
        }

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
