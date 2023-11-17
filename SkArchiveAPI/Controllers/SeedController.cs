using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkArchiveDB;
using System.Net.Http.Headers;

namespace SkArchiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly SkArchiveDbContext _db;
        private readonly string _pathName;

        public SeedController(SkArchiveDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _pathName = Path.Combine(environment.ContentRootPath, "Data/brandproducts.csv");
        }

        [HttpPost("Brands")]
        public async Task<IActionResult> ImportBrandsAsync()
        {
            // create a lookup dictionary containg all of the countries already existing
            // into the database (it will be empty on first run
            Dictionary<string, Brand> brandsByName = _db.Brands.AsNoTracking()
                .ToDictionary(x =.x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HttpHeadersNonValidated = null
            };
        }
    }
}
