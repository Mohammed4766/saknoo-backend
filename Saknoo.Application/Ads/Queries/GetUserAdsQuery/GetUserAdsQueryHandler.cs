using AutoMapper;
using MediatR;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Application.Common;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Ads.Queries.GetUserAdsQuery;




public class GetUserAdsQueryHandler(IAdRepository adRepository, IMapper mapper) : IRequestHandler<GetUserAdsQuery, PagedResult<AdDto>>
{


    public async Task<PagedResult<AdDto>> Handle(GetUserAdsQuery request, CancellationToken cancellationToken)
    {
        var (ads, totalCount) = await adRepository.GetAdsByUserIdAsync(request.UserId, request.PageNumber, request.PageSize);
        var adDto = mapper.Map<List<AdDto>>(ads);
        return new PagedResult<AdDto>(adDto, totalCount, request.PageSize, request.PageNumber);
    }
}

