using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Abstractions
{
    public interface IAuthDbContext
    {
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
