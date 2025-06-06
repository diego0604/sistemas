using Application.DTOs;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sistemas.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var user = await _authService.AuthenticateAsync(dto);
            if (user is null)
                return Unauthorized(new { message = "Credenciales inválidas" });
            return Ok(user);
        }
    }
}
