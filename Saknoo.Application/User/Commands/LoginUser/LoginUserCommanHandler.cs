using MediatR;
using Microsoft.AspNetCore.Identity;
using Saknoo.Application.User.Dtos;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;

namespace Saknoo.Application.User.LoginUser
{
    public class LoginUserCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        : IRequestHandler<LoginUserCommand, AuthResultDto>
    {
        public async Task<AuthResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.PhoneNumber);
            if (user == null)
            {
                return new AuthResultDto
                {
                    Succeeded = false,
                    Errors = new[] {"User not found or Invalid password."}
                };
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return new AuthResultDto
                {
                    Succeeded = false,
                    Errors = new[] {"User not found or Invalid password."}
                };
            }

            var (accessToken, refreshToken) = await tokenService.GenerateTokensAsync(user);

            return new AuthResultDto
            {
                Succeeded = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
