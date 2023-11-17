using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkArchiveDB;

[Table("brands")]
public partial class Brand
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("iso2")]
    [StringLength(2)]
    [Unicode(false)]
    public string Iso2 { get; set; } = null!;

    [Column("iso3")]
    [StringLength(3)]
    [Unicode(false)]
    public string Iso3 { get; set; } = null!;

    [InverseProperty("IdNavigation")]
    public virtual Product? Product { get; set; }
}
