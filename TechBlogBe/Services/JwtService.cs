using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TechBlogBe.Models;

namespace TechBlogBe.Services;

public class JwtService
{
    private const int EXPIRATION_MINUTES = 60 * 24 * 10; // 10 days
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AuthenticationResponse CreateToken(User user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);
        var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials(), expiration);
        var tokenHandler = new JwtSecurityTokenHandler();

        return new AuthenticationResponse
        {
            Token = tokenHandler.WriteToken(token),
            // RefreshToken = CreateRefreshToken(),
            Expiration = expiration
        };
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token,
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidIssuer = _configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            },
            out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            return null;
        }

        return principal;
    }

    private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims,
        SigningCredentials credentials,
        DateTime expiration) =>
        new(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: credentials);

    private static IEnumerable<Claim> CreateClaims(User user) => new[]
    {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email)
    };

    private SigningCredentials CreateSigningCredentials() => new(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
        SecurityAlgorithms.HmacSha256);
}