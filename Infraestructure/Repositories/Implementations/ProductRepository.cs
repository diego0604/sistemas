using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Abstractions;
using Domain.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly IAppDbContext _context;

    public ProductRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await (_context as DbContext)!.Set<Product>().FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(string? searchTerm = null, string? category = null)
    {
        IQueryable<Product> query = (_context as DbContext)!.Set<Product>();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            var lower = searchTerm.ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(lower));
        }

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category == category);
        }

        return await query.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await (_context as DbContext)!.Set<Product>().AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        (_context as DbContext)!.Set<Product>().Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        (_context as DbContext)!.Set<Product>().Remove(product);
        await _context.SaveChangesAsync();
    }
}
