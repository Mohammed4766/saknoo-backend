using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Ads.Queries.GetAdByIdQuery;

public class GetAdByIdHandler(
    IAdRepository adRepository,
    IMapper mapper,
    ILogger<GetAdByIdHandler> logger
) : IRequestHandler<GetAdByIdQuery, AdDto>
{
    public async Task<AdDto> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetAdByIdQuery for AdId: {AdId}", request.AdId);

        var ad = await adRepository.GetByIdAsync(request.AdId);
        if (ad is null)
        {
            logger.LogWarning("Ad with ID {AdId} not found.", request.AdId);
            throw new Exception("Ad not found");
        }

        var adDto = mapper.Map<AdDto>(ad);

        logger.LogInformation("Successfully retrieved ad with ID: {AdId}", ad.Id);

        return adDto;
    }
}
