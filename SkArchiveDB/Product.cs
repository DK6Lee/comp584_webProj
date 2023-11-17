using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkArchiveDB;

[Table("products")]
public partial class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("category")]
    [StringLength(50)]
    [Unicode(false)]
    public string Category { get; set; } = null!;

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("pic", TypeName = "image")]
    public byte[] Pic { get; set; } = null!;

    [Column("brand_id")]
    public int BrandId { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Product")]
    public virtual Brand IdNavigation { get; set; } = null!;
}
