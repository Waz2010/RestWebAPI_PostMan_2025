using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPCoreWebAPI.Models;
using ASPCoreWebAPI.Models.DTO;
using ASPCoreWebAPI.Services;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsersDBContext _context;
        private readonly IConfiguration _config;

        public AuthController(UsersDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            // Hash the incoming plain password using the same logic as during registration
            var hashedPassword = HashPassword(login.Password);

            var user = await _context.AspNetUsers
                .FirstOrDefaultAsync(u => u.UserName == login.Username && u.PasswordHash == hashedPassword);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = TokenService.GenerateToken(user, _config);
            return Ok(new { token });
        }

        // Password hash method (same as registration)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}


