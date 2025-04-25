using Saknoo.Domain.Entities;

namespace Saknoo.Domain.Interfaces;

public interface ITokenService
{
    Task<(string accessToken, string refreshToken)> GenerateTokensAsync(ApplicationUser user);
    Task<string?> GetStoredRefreshTokenAsync(ApplicationUser user);
    Task RemoveRefreshTokenAsync(ApplicationUser user);
}