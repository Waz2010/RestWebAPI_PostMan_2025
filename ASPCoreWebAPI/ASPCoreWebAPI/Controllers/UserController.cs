using ASPCoreWebAPI.Models;
using ASPCoreWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ASPCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UsersDBContext _context;

        public UserController(UsersDBContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            if (await _context.AspNetUsers.AnyAsync(u => u.UserName == userDto.UserName))
                return BadRequest("Username already exists.");

            var newUser = new AspNetUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userDto.UserName,
                Email = userDto.Email,
                PasswordHash = HashPassword(userDto.Password),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            _context.AspNetUsers.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully" });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
