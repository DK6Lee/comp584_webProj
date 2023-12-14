using Microsoft.Identity.Client;
using SkArchiveDB;

namespace SkArchiveAPI.DTOs
{
    public class BrandCountry
    {
        public string Iso2 { get; set; } = null!;
        public string Iso3 { get; set;} = null!;
        public string Country {  get; set; } = null!;
        public string BrandName { get; set; } = null!;
    }
}
