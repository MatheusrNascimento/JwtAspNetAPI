using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtAspNet.Services
{
    public class TokenService
    {
        public string CreateToken()
        {
            byte[] key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
            var handler = new JwtSecurityTokenHandler();

            var credentials  = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256);
            
            var tokenDescriptor = new SecurityTokenDescriptor
                {
                    SigningCredentials = credentials,
                    Expires = DateTime.UtcNow.AddDays(2)
                };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}