using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using SkArchiveDB;
using SkArchiveApi.Data;
using Microsoft.AspNetCore.Identity;

namespace SkArchiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly SkArchiveDbContext _db;
        private readonly UserManager<SkArchiveUser> _userManager;
        private readonly string _pathName;

        public UserManager<SkArchiveUser> UserManager => _userManager;

        public SeedController(SkArchiveDbContext db, IWebHostEnvironment environment, UserManager<SkArchiveUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _pathName = Path.Combine(environment.ContentRootPath, "Data/SkArchiveCsv.csv");
        }

        [HttpPost("Users")]
        public async Task<IActionResult> ImportUsersAsync()
        {
            (string name, string email) = ("user1", "user@email.com");
            SkArchiveUser user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            if(await _userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user2";
            }
            _ = await _userManager.CreateAsync(user, "P@ssw0rd!") ?? throw new InvalidOperationException();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await _db.SaveChangesAsync();
            return Ok();
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
                    Name = record.brand,
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
                        Console.WriteLine($"Not found brand for {record.product}");
                        return NotFound(record);
                    }

                    Product product = new()
                    {
                        Name = record.product,
                        Category = record.category,
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
