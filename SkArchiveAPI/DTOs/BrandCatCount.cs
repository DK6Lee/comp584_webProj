using Microsoft.Identity.Client;
using SkArchiveDB;

namespace SkArchiveAPI.DTOs
{
    public class BrandCatCount
    {
        public int Id { get; set; }
        public string BrandName { get; set; } = null!;
        public int CategoryCount { get; set; }
    }
}
