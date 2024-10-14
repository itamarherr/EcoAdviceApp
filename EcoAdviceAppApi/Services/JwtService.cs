using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using System.Text;

namespace EcoAdviceAppApi.Services
{
    public class JwtService(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        public async Task<string> CreateToken(AppUser user)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new Exception("secret key must be set in app setting");
            var claims = new List<Claim>
            {
               new(ClaimTypes.NameIdentifier, user.Id.ToString()), 
               //new(ClaimTypes.Email, user.Email),
               new(ClaimTypes.Name, user.UserName)
             };
            var isAdmin = await userManager.IsInRoleAsync(user, "admin");
            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: jwtSettings["Issuer"],
               audience: jwtSettings["Audience"],
               expires: DateTime.UtcNow.AddDays(1),
               claims: claims,
               signingCredentials: creds
             );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }   
    }
}
