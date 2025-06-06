using Application.DTOs;
using Application.Services.Abstractions;
using Domain.Repositories.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> AuthenticateAsync(LoginRequestDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null || !user.IsActive) return null;

        // Suponemos un hash simple: SHA256 de la contraseña en texto plano.
        // En producción, usa sal + hash + pepper.
        var hashed = ComputeSha256Hash(loginDto.Password);
        if (user.PasswordHash != hashed) return null;

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }

    private static string ComputeSha256Hash(string raw)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
        return Convert.ToHexString(bytes);
    }
}
