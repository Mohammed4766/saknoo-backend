using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Saknoo.Application.User.Dtos;
using Saknoo.Domain.Entities;

namespace Saknoo.Application.User.Commands.UpdateUserCommand;


public class UpdateUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<UpdateUserCommandHandler> logger,
    IUserContext userContext
) : IRequestHandler<UpdateUserCommand, bool>
{
    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()
            ?? throw new UnauthorizedAccessException("User must be authenticated.");

        logger.LogInformation("Trying to update user with ID: {UserId}", currentUser.UserId);

        var user = await userManager.FindByIdAsync(currentUser.UserId);
        if (user == null)
        {
            logger.LogWarning("User not found with ID: {UserId}", currentUser.UserId);
            return false;
        }

        if (!string.IsNullOrEmpty(request.FullName))
            user.FullName = request.FullName;

        if (request.Bio != null)
            user.Bio = request.Bio;

        var result = await userManager.UpdateAsync(user);

        if (result.Succeeded)
            logger.LogInformation("User updated successfully: {UserId}", currentUser.UserId);
        else
            logger.LogError("Failed to update user: {UserId}, Errors: {Errors}", currentUser.UserId,
                string.Join(", ", result.Errors.Select(e => e.Description)));

        return result.Succeeded;
    }
}


