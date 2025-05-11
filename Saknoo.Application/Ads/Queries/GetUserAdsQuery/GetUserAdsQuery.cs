using MediatR;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Application.Common;

namespace Saknoo.Application.Ads.Queries.GetUserAdsQuery;


public class GetUserAdsQuery : IRequest<PagedResult<AdDto>>
{
    public string UserId { get; set; } = null!;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

