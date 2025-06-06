using Domain.Repositories.Abstractions;
using global::Infrastructure.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{


    public class DashboardRepository : IDashboardRepository
    {
        private readonly IAppDbContext _context;

        public DashboardRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            // Sumamos la columna TotalAmount de Sales
            return await (_context as DbContext)!
                         .Set<Domain.Entities.Sale>()
                         .SumAsync(s => s.TotalAmount);
        }

        public async Task<int> GetTotalProductsSoldAsync()
        {
            // Sumamos la columna Quantity de SaleItems
            return await (_context as DbContext)!
                         .Set<Domain.Entities.SaleItem>()
                         .SumAsync(si => si.Quantity);
        }

        public async Task<int> GetSalesCountAsync()
        {
            // Contamos las filas de Sales
            return await (_context as DbContext)!
                         .Set<Domain.Entities.Sale>()
                         .CountAsync();
        }


    }
}

