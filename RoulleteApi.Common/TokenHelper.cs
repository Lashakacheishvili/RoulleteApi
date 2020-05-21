using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RoulleteApi.Common
{
    public class TokenHelper
    {
        public string CreateToken(string key, DateTime expires, params Claim[] claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyInt = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyInt), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenObject = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenObject);
        }
    }
}
