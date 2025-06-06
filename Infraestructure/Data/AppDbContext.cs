
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=appdb;Port=3306;Database=FarmaciaDb;Uid=root;Pwd=RootPassword123!;SslMode=None");
        }

        public DbSet<SaleItem> SaleItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }

}
