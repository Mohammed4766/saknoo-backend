using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Saknoo.Application.User.Dtos;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;
using Saknoo.Infrastructure.Data;

namespace Saknoo.Application.User._RefreshToken;

public class RefreshTokenCommandHandler(
    UserManager<ApplicationUser> userManager,
    ITokenService tokenService,
    ApplicationDbContext dbContext,
    ILogger<RefreshTokenCommandHandler> logger   
) : IRequestHandler<RefreshTokenCommand, AuthResultDto>
{
    public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("RefreshTokenCommand started for token: {RefreshToken}", request.RefreshToken);

        var tokenEntity = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && !rt.IsUsed && !rt.IsRevoked && rt.Expires > DateTime.UtcNow, cancellationToken);

        if (tokenEntity is null)
        {
            logger.LogWarning("Invalid or expired refresh token attempted: {RefreshToken}", request.RefreshToken);
            return new AuthResultDto { Succeeded = false, Errors = new[] { "Invalid refresh token." } };
        }

        tokenEntity.IsUsed = true;
        dbContext.RefreshTokens.Update(tokenEntity);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Refresh token marked as used for user: {UserId}", tokenEntity.User.Id);

        var (newAccessToken, newRefreshToken) = await tokenService.GenerateTokensAsync(tokenEntity.User);

        logger.LogInformation("New access and refresh tokens generated for user: {UserId}", tokenEntity.User.Id);

        return new AuthResultDto
        {
            Succeeded = true,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
