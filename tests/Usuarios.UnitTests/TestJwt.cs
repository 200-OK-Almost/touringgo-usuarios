using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Usuarios.UnitTests;

public static class TestJwt
{
    public const string Issuer = "TouringGo.Usuarios";
    public const string Audience = "TouringGo";
    public static readonly string Key = Convert.ToBase64String(new byte[32]);

    public static string GenerarToken(bool incluirSub, string? sub = null)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (incluirSub)
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, sub ?? Guid.NewGuid().ToString()));

        var signingKey = new SymmetricSecurityKey(Convert.FromBase64String(Key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
