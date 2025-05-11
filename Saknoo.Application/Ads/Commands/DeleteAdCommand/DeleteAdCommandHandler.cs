using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Application.User;
using Saknoo.Domain.Repositories;
using Saknoo.Domain.Exceptions;

namespace Saknoo.Application.Ads.Commands.DeleteAdCommand;

public class DeleteAdCommandHandler(
    IAdRepository adRepository,
    IUserContext userContext,
    ILogger<DeleteAdCommandHandler> logger
) : IRequestHandler<DeleteAdCommand, bool>
{
    public async Task<bool> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to delete ad with ID: {AdId}", request.Id);

        var currentUser = userContext.GetCurrentUser()
            ?? throw new UnauthorizedAccessException("User must be authenticated.");

        var ad = await adRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Ad with ID {request.Id} was not found.");

        if (ad.UserId != currentUser.UserId)
            throw new ForbiddenException("You are not allowed to delete this ad.");

        await adRepository.DeleteAsync(ad);

        logger.LogInformation("Ad with ID: {AdId} successfully deleted by user ID: {UserId}", ad.Id, currentUser.UserId);

        return true;
    }
}
