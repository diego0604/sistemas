using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Abstractions;
using Domain.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;


public class SalesRepository : ISalesRepository
{
    private readonly IAppDbContext _context;

    public SalesRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateSaleAsync(Sale sale)
    {
        await (_context as DbContext)!.Set<Sale>().AddAsync(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<IEnumerable<Sale>> GetAllSalesAsync(DateTime? from = null, DateTime? to = null)
    {
        IQueryable<Sale> query = (_context as DbContext)!.Set<Sale>()
            .Include(s => s.Items)
            .ThenInclude(si => si.Product);

        if (from.HasValue)
            query = query.Where(s => s.SaleDate >= from.Value);

        if (to.HasValue)
            query = query.Where(s => s.SaleDate <= to.Value);

        return await query.OrderByDescending(s => s.SaleDate).ToListAsync();
    }

    public async Task<IEnumerable<SaleItem>> GetSaleItemsBySaleIdAsync(int saleId)
    {
        return await (_context as DbContext)!.Set<SaleItem>()
            .Where(si => si.SaleId == saleId)
            .Include(si => si.Product)
            .ToListAsync();
    }
}
