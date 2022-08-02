using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFisrtAPIAuthorization.Services
{
    public class AuthenticationService : IAutheticationService
    {
        public string key;

        public AuthenticationService(string key)
        {
            this.key = key;
        }

        public async Task<string> AuthenticateAsync(string userName, string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(7),
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier, id)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDecriptor);

            return await Task.Run(() => tokenHandler.WriteToken(token));
        }
    }
}
