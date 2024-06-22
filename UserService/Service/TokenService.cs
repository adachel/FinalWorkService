using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserService.DB;
using UserService.DTO;

namespace UserService.Service
{
    public static class TokenService
    {
        public static string GenerateToken(UserModel user, IConfiguration config, UserContext context)
        {
            var key = new RsaSecurityKey(RSATools.GetPrivatKey());
            var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),

            };

            using (context) //
            {
                var userContext = context.Users.FirstOrDefault(x => x.Email == user.Email);
                claims.Add(new Claim("Id", userContext.Id.ToString()));
            }

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
