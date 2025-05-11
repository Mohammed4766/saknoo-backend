using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Application.User;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Repositories;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Domain.Exceptions;

namespace Saknoo.Application.Ads.Commands.UpdateAdCommand;

public class UpdateAdCommandHandler(
    IAdRepository adRepository,
    IUserContext userContext,
    IMapper mapper,
    ILogger<UpdateAdCommandHandler> logger
) : IRequestHandler<UpdateAdCommand, Guid>
{
    public async Task<Guid> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating ad with id: {AdId}", request.Id);

        var currentUser = userContext.GetCurrentUser()
            ?? throw new UnauthorizedAccessException("User must be authenticated.");

        var originalAd = await adRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Ad with ID {request.Id} was not found.");

        if (originalAd.UserId != currentUser.UserId)
            throw new ForbiddenException("You are not allowed to update this ad.");

        // Map the update request to a DTO
        var adDto = mapper.Map<UpdateAdDto>(request);

        // Adjust pricing fields based on apartment ownership
        AdjustPricingFields(originalAd.HasApartment, adDto);

        // Validate the DTO
        ValidateAdDto(originalAd.HasApartment, adDto);

        // Map DTO to original entity
        mapper.Map(adDto, originalAd);

        // Update images if provided
        if (adDto.ImageUrls is not null)
        {
            originalAd.Images = adDto.ImageUrls.Select(url => new AdImage
            {
                ImageUrl = url,
                AdId = originalAd.Id
            }).ToList();
        }

        // Update neighborhoods if provided
        if (adDto.NeighborhoodIds is not null)
        {
            originalAd.AdNeighborhoods = adDto.NeighborhoodIds.Select(id => new AdNeighborhood
            {
                NeighborhoodId = id,
                AdId = originalAd.Id
            }).ToList();
        }

        await adRepository.UpdateAsync(originalAd);

        return originalAd.Id;
    }

    private static void AdjustPricingFields(bool hasApartment, UpdateAdDto adDto)
    {
        if (hasApartment)
        {
            adDto.PriceFrom = null;
            adDto.PriceTo = null;
        }
        else
        {
            adDto.Price = null;
        }
    }

    private static void ValidateAdDto(bool hasApartment, UpdateAdDto adDto)
    {
        var validator = new UpdateAdDtoValidator(hasApartment);
        var validationResult = validator.Validate(adDto);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new FluentValidation.ValidationException(string.Join(", ", errors));

        }
    }
}
