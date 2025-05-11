using AutoMapper;
using MediatR;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Application.Common;
using Saknoo.Domain.Interfaces;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Ads.Queries.GetAllAdsQuery;

public class GetAllAdsQueryHandler(
    IAdRepository adRepository,
    IMapper mapper
) : IRequestHandler<GetAllAdsQuery, PagedResult<AdDto>>
{
    public async Task<PagedResult<AdDto>> Handle(GetAllAdsQuery request, CancellationToken cancellationToken)
    {
        var (ads, totalCont) = await adRepository.GetAllMatchingAsync(request.SearschPhrase, request.PageNumber, request.PageSize, request.SortBy, request.SortDirection, request.CityId, request.NeighborhoodIds, request.HasApartment);
        var AdListDto = mapper.Map<List<AdDto>>(ads);



        var ressult = new PagedResult<AdDto>(AdListDto, totalCont, request.PageSize, request.PageNumber);


        return ressult;
    }
}
