using MediatR;
using Saknoo.Application.User;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;
using Saknoo.Domain.Repositories;
using Saknoo.Infrastructure.Services;

namespace Saknoo.Application.Ads.Commands.CreateAdCommand;

public class CreateAdCommandHandler(
    IAdRepository adRepository,
    IUserContext userContext,
    IBlobStorageService blobStorageService) : IRequestHandler<CreateAdCommand, Guid>
{
    public async Task<Guid> Handle(CreateAdCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null)
            throw new UnauthorizedAccessException();

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
            var imageUrl = await blobStorageService.UploadToBlobAsync(stream, $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}");

            ad.Images.Add(new AdImage
            {
                ImageUrl = imageUrl
            });
        }

        await adRepository.CreateAsync(ad);
        return ad.Id;
    }
}

