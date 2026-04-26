using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        // 🔹 REGISTER
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("Username and Password required");

            var userName = model.UserName.Trim().ToLower();

            if (_context.Users.Any(u => u.UserName.ToLower() == userName))
                return BadRequest("Username already exists ❌");

            var user = new User
            {
                UserName = userName,
                PasswordHash = PasswordHelper.HashPassword(model.Password.Trim()),
                Role = userName.ToLower().Contains("admin") ? "Admin" : "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "User registered successfully ✅",
                role = user.Role
            });
        }

        // 🔹 LOGIN
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("Username and Password required");

            var userName = model.UserName.Trim().ToLower();

            var user = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == userName);

            if (user == null)
                return Unauthorized("User not found ❌");

            var hashedPassword = PasswordHelper.HashPassword(model.Password.Trim());

            if (user.PasswordHash != hashedPassword)
                return Unauthorized("Invalid password ❌");

            var jwtKey = _config["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT Key missing");

            var key = Encoding.ASCII.GetBytes(jwtKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                userName = user.UserName,
                role = user.Role
            });
        }

        // 🔹 RESET PASSWORD
        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("Username and Password required");

            var user = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (user == null)
                return NotFound("User not found ❌");

            user.PasswordHash = PasswordHelper.HashPassword(model.Password.Trim());

            _context.SaveChanges();

            return Ok("Password reset successfully ✅");
        }
    }

    public class LoginModel
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}