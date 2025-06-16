using AuthApi.Helpers;
using System.Security.Claims;

namespace AuthApi.Middleware
{
    public class JwtRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public JwtRefreshMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userName = context.User.Identity.Name;
                var role = context.User.FindFirst(ClaimTypes.Role)?.Value;

                var newToken = JwtHelper.GenerateToken(userName, role, _config["Jwt:Key"]);

                context.Response.Cookies.Append("jwt", newToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15)
                });
            }

            await _next(context);
        }
    }

}
