using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Application.User;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;
using Saknoo.Domain.Repositories;
using Saknoo.Infrastructure.Services;

namespace Saknoo.Application.Ads.Commands.CreateAdCommand;

public class CreateAdCommandHandler(
    IAdRepository adRepository,
    IUserContext userContext,
    IBlobStorageService blobStorageService,
    ILogger<CreateAdCommandHandler> logger
    ) : IRequestHandler<CreateAdCommand, Guid>
{
    public async Task<Guid> Handle(CreateAdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting execution of CreateAdCommand");

        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null)
        {
            logger.LogWarning("Unauthorized request: current user is null");
            throw new UnauthorizedAccessException();
        }

        logger.LogInformation("Creating a new ad for user: {UserId}", currentUser.UserId);

        var ad = new Ad
        {
            Id = Guid.NewGuid(),
            UserId = currentUser.UserId,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            PriceFrom = request.PriceFrom,
            PriceTo = request.PriceTo,
            HasApartment = request.HasApartment,
            CityId = request.CityId,
        };

        ad.AdNeighborhoods = request.NeighborhoodIds.Select(id => new AdNeighborhood
        {
            NeighborhoodId = id
        }).ToList();

        foreach (var image in request.Images)
        {
            using var stream = image.OpenReadStream();
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var imageUrl = await blobStorageService.UploadToBlobAsync(stream, fileName);

            logger.LogDebug("Uploaded image for ad: {ImageUrl}", imageUrl);

            ad.Images.Add(new AdImage
            {
                ImageUrl = imageUrl
            });
        }

        await adRepository.CreateAsync(ad);

        logger.LogInformation("Ad created successfully: {AdId}", ad.Id);

        return ad.Id;
    }
}


