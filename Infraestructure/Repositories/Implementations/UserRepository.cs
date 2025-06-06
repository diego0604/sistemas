using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Abstractions;
using Domain.Repositories.Abstractions;
using Infrastructure.Data.Infrastructure.Data;

namespace Infrastructure.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly IAuthDbContext _context;


    public UserRepository(IAuthDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {

        return await (_context as DbContext)!.Set<User>()
                    .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await (_context as DbContext)!.Set<User>().FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync(string? searchTerm = null)
    {
        IQueryable<User> query = (_context as DbContext)!.Set<User>();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            var lower = searchTerm.ToLower();
            query = query.Where(u =>
                u.Name.ToLower().Contains(lower) ||
                u.Email.ToLower().Contains(lower)
            );
        }

        return await query.OrderBy(u => u.Name).ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await (_context as DbContext)!.Set<User>().AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        (_context as DbContext)!.Set<User>().Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        (_context as DbContext)!.Set<User>().Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailExistsAsync(string email, int? excludeUserId = null)
    {
        var q = (_context as DbContext)!.Set<User>().AsQueryable();
        if (excludeUserId.HasValue)
        {
            q = q.Where(u => u.Email == email && u.Id != excludeUserId.Value);
        }
        else
        {
            q = q.Where(u => u.Email == email);
        }
        return await q.AnyAsync();
    }
}