using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Application.Common;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Ads.Queries.GetUserAdsQuery;

public class GetUserAdsQueryHandler(
    IAdRepository adRepository,
    IMapper mapper,
    ILogger<GetUserAdsQueryHandler> logger
) : IRequestHandler<GetUserAdsQuery, PagedResult<AdDto>>
{
    public async Task<PagedResult<AdDto>> Handle(GetUserAdsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting ads for user {UserId}, Page {PageNumber}, Size {PageSize}", 
            request.UserId, request.PageNumber, request.PageSize);

        var (ads, totalCount) = await adRepository.GetAdsByUserIdAsync(request.UserId, request.PageNumber, request.PageSize);
        var adDto = mapper.Map<List<AdDto>>(ads);
        var result = new PagedResult<AdDto>(adDto, totalCount, request.PageSize, request.PageNumber);

        logger.LogInformation("Retrieved {Count} ads out of total {TotalCount} for user {UserId}", 
            adDto.Count, totalCount, request.UserId);

        return result;
    }
}
