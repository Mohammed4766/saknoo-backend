// Application/User/RefreshToken/RefreshTokenCommandHandler.cs
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saknoo.Application.User.Dtos;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Saknoo.Infrastructure.Data;

namespace Saknoo.Application.User._RefreshToken;

public class RefreshTokenCommandHandler(
    UserManager<ApplicationUser> userManager,
    ITokenService tokenService,
    ApplicationDbContext dbContext
) : IRequestHandler<RefreshTokenCommand, AuthResultDto>
{
    public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenEntity = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && !rt.IsUsed && !rt.IsRevoked && rt.Expires > DateTime.UtcNow, cancellationToken);

        if (tokenEntity is null)
        {
            return new AuthResultDto { Succeeded = false, Errors = new[] { "Invalid refresh token." } };
        }

        tokenEntity.IsUsed = true;
        dbContext.RefreshTokens.Update(tokenEntity);
        await dbContext.SaveChangesAsync(cancellationToken);

        var (newAccessToken, newRefreshToken) = await tokenService.GenerateTokensAsync(tokenEntity.User);

        return new AuthResultDto
        {
            Succeeded = true,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
