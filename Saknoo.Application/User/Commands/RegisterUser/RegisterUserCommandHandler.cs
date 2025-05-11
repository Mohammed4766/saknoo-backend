using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saknoo.Application.User.Dtos;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;

namespace Saknoo.Application.User.RegisterUser;

public class RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService) : IRequestHandler<RegisterUserCommand, AuthResultDto>
{

    public async Task<AuthResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
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
            );

            return new AuthResultDto { Succeeded = false, Errors = result.Errors.Select(e => e.Description) };
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
