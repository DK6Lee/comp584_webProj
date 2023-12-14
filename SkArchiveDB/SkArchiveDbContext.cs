using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SkArchiveDB;

public partial class SkArchiveDbContext : IdentityDbContext<SkArchiveUser>
{
    public SkArchiveDbContext()
    {
    }

    public SkArchiveDbContext(DbContextOptions<SkArchiveDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // if already configured, don't override
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.Property(e => e.Iso2).IsFixedLength();
            entity.Property(e => e.Iso3).IsFixedLength();
            entity.Property(e => e.Country).IsFixedLength();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Name).IsFixedLength();

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_products_brands");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
