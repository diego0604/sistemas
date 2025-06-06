using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Sale> Sales { get; }
        DbSet<SaleItem> SaleItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
