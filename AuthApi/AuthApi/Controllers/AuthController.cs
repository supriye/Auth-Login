using AuthApi.Helpers;
using AuthApi.Interfaces;
using AuthApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.ValidateUser(request.Username, request.Password);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = JwtHelper.GenerateToken(user.Username, user.Role, _config["Jwt:Key"]);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15)
            });

            return Ok(new
            {
                message = "Login successful",
                username = user.Username,
                role = user.Role,
                token = token
            });
        }
    }
}
