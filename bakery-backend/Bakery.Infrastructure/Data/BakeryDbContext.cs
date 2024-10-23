using Bakery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Infrastructure.Data;

public class BakeryDbContext(DbContextOptions<BakeryDbContext> options) : DbContext(options)
{
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(SeedData.Products);

        modelBuilder.Entity<Sale>(eb =>
        {
            eb.HasData(SeedData.Sales);
            eb.HasQueryFilter(sale => sale.IsActive);
        });
    }
}