using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Saknoo.Application.User.Dtos;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;

namespace Saknoo.Application.User.RegisterUser;

public class RegisterUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    ITokenService tokenService,
    ILogger<RegisterUserCommandHandler> logger  
) : IRequestHandler<RegisterUserCommand, AuthResultDto>
{
    public async Task<AuthResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting registration for phone number: {PhoneNumber}", request.PhoneNumber);

        var user = new ApplicationUser
        {
            PhoneNumber = request.PhoneNumber,
            UserName = request.PhoneNumber,
            NationalityId = request.NationalityId,
            FullName = request.FullName,
            Gender = request.Gender,
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e =>
                e.Code == "DuplicateUserName"
                ? "Phone number is already in use."
                : e.Description
            ).ToList();

            logger.LogWarning("Registration failed for {PhoneNumber}. Errors: {Errors}", request.PhoneNumber, string.Join(", ", errors));

            return new AuthResultDto { Succeeded = false, Errors = errors };
        }

        logger.LogInformation("User registered successfully with phone number: {PhoneNumber}", request.PhoneNumber);

        var (accessToken, refreshToken) = await tokenService.GenerateTokensAsync(user);

        return new AuthResultDto
        {
            Succeeded = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
