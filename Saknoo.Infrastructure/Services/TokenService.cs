using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;
using Saknoo.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Saknoo.Infrastructure.Services;

public class TokenService(
    ApplicationDbContext dbContext,
    IConfiguration configuration,
    UserManager<ApplicationUser> userManager) : ITokenService
{
    public async Task<(string accessToken, string refreshToken)> GenerateTokensAsync(ApplicationUser user)
    {
        var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!);
        var tokenHandler = new JwtSecurityTokenHandler();

        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration["JwtSettings:Issuer"],
            Audience = configuration["JwtSettings:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        dbContext.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            Expires = DateTime.UtcNow.AddDays(7),
            IsUsed = false,
            IsRevoked = false
        });

        await dbContext.SaveChangesAsync();

        return (accessToken, refreshToken);
    }

    public Task<string?> GetStoredRefreshTokenAsync(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRefreshTokenAsync(ApplicationUser user)
    {
        throw new NotImplementedException();
    }
}
