using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkArchiveDB;
public partial class Brand
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string Iso2 { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string Iso3 { get; set; } = null!;

    [StringLength(50)]
    public string Country { get; set; } = null!;  

    [InverseProperty("Brand")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
