using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Application.Common;
using Saknoo.Domain.Interfaces;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Ads.Queries.GetAllAdsQuery;

public class GetAllAdsQueryHandler(
    IAdRepository adRepository,
    IMapper mapper,
    ILogger<GetAllAdsQueryHandler> logger
) : IRequestHandler<GetAllAdsQuery, PagedResult<AdDto>>
{
    public async Task<PagedResult<AdDto>> Handle(GetAllAdsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetAllAdsQuery: Page {PageNumber}, Size {PageSize}, CityId: {CityId}, HasApartment: {HasApartment}, Search: {Search}",
            request.PageNumber, request.PageSize, request.CityId, request.HasApartment, request.SearschPhrase);

        var (ads, totalCount) = await adRepository.GetAllMatchingAsync(
            request.SearschPhrase,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection,
            request.CityId,
            request.NeighborhoodIds,
            request.HasApartment
        );

        var adListDto = mapper.Map<List<AdDto>>(ads);
        var result = new PagedResult<AdDto>(adListDto, totalCount, request.PageSize, request.PageNumber);

        logger.LogInformation("Retrieved {Count} ads out of total {TotalCount}.", adListDto.Count, totalCount);

        return result;
    }
}
