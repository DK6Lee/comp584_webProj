using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using SkArchiveDB;
using SkArchiveAapi.Data;

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
            _pathName = Path.Combine(environment.ContentRootPath, "Data/SkArchiveCsv.csv");
        }

        [HttpPost("Brands")]
        public async Task<IActionResult> ImportBrandsAsync()
        {
            // create a lookup dictionary containg all of the countries already existing
            // into the database (it will be empty on first run
            Dictionary<string, Brand> brandsByName = _db.Brands.AsNoTracking()
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            IEnumerable<SkArchiveCsv>? records = csv.GetRecords<SkArchiveCsv>().ToList();
            foreach (SkArchiveCsv record in records)
            {
                if (brandsByName.ContainsKey(record.brand))
                {
                    continue;
                }

                Brand brand = new()
                {
                    Name = record.country,
                    Iso2 = record.iso2,
                    Iso3 = record.iso3
                };
                await _db.Brands.AddAsync(brand);
                brandsByName.Add(record.brand, brand);
            }
            await _db.SaveChangesAsync();

            return new JsonResult(brandsByName.Count);
        }

        [HttpPost("Products")]
        public async Task<IActionResult> ImportProducts()
        {
            Dictionary<String, Brand> brands = await _db.Brands.AsNoTracking()
                .ToDictionaryAsync(c => c.Name);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            int productCount = 0;
            using (StreamReader reader = new(_pathName))
                using (CsvReader csv = new(reader, config))
            {
                IEnumerable<SkArchiveCsv>? records = csv.GetRecords<SkArchiveCsv>();
                foreach (SkArchiveCsv record in records)
                {
                    if (!brands.ContainsKey(record.brand))
                    {
                        Console.WriteLine($"Not found country for {record.product}");
                        return NotFound(record);
                    }

                    Product product = new()
                    {
                        Name = record.product,
                        Category = record.category,
                        Description = record.description,
                        BrandId = brands[record.product].Id
                    };
                    _db.Products.Add(product);
                    productCount++;
                }
                await _db.SaveChangesAsync();
            }
            return new JsonResult(productCount);
        }

    }
}
