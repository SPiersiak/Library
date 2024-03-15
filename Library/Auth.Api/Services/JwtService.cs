using Auth.Api.Commands;
using Auth.Api.Models;
using Auth.Api.Services.Interfaces;
using Commons.Extension;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Services;

public class JwtService : IJwtService
{
    public AuthenticationToken? GenerateAuthToken(SignInCommand user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtExtension.SecurityKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var expirationTimeStamp = DateTime.Now.AddMinutes(5);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
        };
        var tokenOptions = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            claims: claims,
            expires: expirationTimeStamp,
            signingCredentials: signingCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new AuthenticationToken() { Token = tokenString, ExpiresIn = (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds };
    }
}
