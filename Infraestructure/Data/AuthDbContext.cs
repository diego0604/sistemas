using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using global::Infrastructure.Data.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Data
{
    

    namespace Infrastructure.Data
    {
        public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options), IAuthDbContext
        {
            public DbSet<User> Users { get; set; } = null!;

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseMySQL("Server=authdb;Port=3306;Database=AuthDb;Uid=root;Pwd=RootPassword123!;SslMode=None");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

                static string Hash123()
                {
                    using var sha256 = SHA256.Create();
                    var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("123"));
                    return Convert.ToHexString(bytes);
                }

                var passwordHash = Hash123();

                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        Name = "diego",
                        Email = "diego@farmacia.com",
                        PasswordHash = passwordHash,
                        Role = "admin", 
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        Id = 2,
                        Name = "juan",
                        Email = "juan@farmacia.com",
                        PasswordHash = passwordHash,
                        Role = "worker", 
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    }
                );

                base.OnModelCreating(modelBuilder);
            }
        }
    }


}
