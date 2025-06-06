using Domain.Entities;

namespace Domain.Repositories.Abstractions
{
 
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync(string? searchTerm = null);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<bool> EmailExistsAsync(string email, int? excludeUserId = null);
    }

}
