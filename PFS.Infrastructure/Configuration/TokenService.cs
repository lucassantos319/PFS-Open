using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PFS.Domain.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace PFS.Infrastrucuture.Services;

public static class TokenService
{
    private static readonly Guid _guid = new Guid();

    public static string Generate(Users user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("e5ea3b5c-f67f-45f4-931f-13138caa29cb");
        var credentials = new SigningCredentials(
        new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = credentials,
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(Users user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.email));
        return ci;
    }
}