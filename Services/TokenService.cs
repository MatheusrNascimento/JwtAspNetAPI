using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAspNet.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtAspNet.Services
{
    public class TokenService
    {
        public string CreateToken(User user)
        {
            byte[] key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
            var handler = new JwtSecurityTokenHandler();

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(2),
                Subject = GenerateClaims(user)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            ClaimsIdentity ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("id", user.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            ci.AddClaim(new Claim("image", user.Image));

            foreach(string role in user.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role));

            return ci;
        }
    }
}