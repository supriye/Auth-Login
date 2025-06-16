using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateToken(string userName, string role, string secret)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(15),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}