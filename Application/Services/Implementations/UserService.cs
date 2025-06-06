using Application.DTOs;
using Application.Services.Abstractions;
using Domain.Entities;
using Domain.Repositories.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        User? user = await _userRepository.GetByIdAsync(id);
        if (user is null) return null;
        return MapToDto(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync(string? searchTerm = null)
    {
        var users = await _userRepository.GetAllAsync(searchTerm);
        return users.Select(MapToDto);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        if (await _userRepository.EmailExistsAsync(dto.Email, null))
            throw new Exception("El email ya existe.");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = ComputeSha256Hash(dto.Password),
            Role = dto.Role,
            IsActive = true
        };

        await _userRepository.AddAsync(user);
        return MapToDto(user);
    }

    public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        if (dto.Email != null && await _userRepository.EmailExistsAsync(user.Email, id))
            throw new Exception("El email ya existe.");

        user.Name = dto.Name;
        if (!string.IsNullOrEmpty(dto.Password))
        {
            user.PasswordHash = ComputeSha256Hash(dto.Password);
        }
        user.Role = dto.Role;
        user.IsActive = dto.IsActive;

        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return false;
        user.IsActive = false;
        await _userRepository.UpdateAsync(user);
        return true;
    }

    private static UserDto MapToDto(User user) =>
        new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };

    private static string ComputeSha256Hash(string raw)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
        return Convert.ToHexString(bytes);
    }
}
