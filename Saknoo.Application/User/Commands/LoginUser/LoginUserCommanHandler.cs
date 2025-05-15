using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Saknoo.Application.User.Dtos;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;

namespace Saknoo.Application.User.LoginUser
{
    public class LoginUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        ILogger<LoginUserCommandHandler> logger 
    ) : IRequestHandler<LoginUserCommand, AuthResultDto>
    {
        public async Task<AuthResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Login attempt for user: {PhoneNumber}", request.PhoneNumber);

            var user = await userManager.FindByNameAsync(request.PhoneNumber);
            if (user == null)
            {
                logger.LogWarning("Login failed: User not found for phone number {PhoneNumber}", request.PhoneNumber);
                return new AuthResultDto
                {
                    Succeeded = false,
                    Errors = new[] { "User not found or Invalid password." }
                };
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                logger.LogWarning("Login failed: Invalid password for user {UserId}", user.Id);
                return new AuthResultDto
                {
                    Succeeded = false,
                    Errors = new[] { "User not found or Invalid password." }
                };
            }

            var (accessToken, refreshToken) = await tokenService.GenerateTokensAsync(user);

            logger.LogInformation("Login successful for user {UserId}", user.Id);

            return new AuthResultDto
            {
                Succeeded = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
