using System;
using MediatR;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Application.Common;
using Saknoo.Domain.Constants;


namespace Saknoo.Application.Ads.Queries.GetAllAdsQuery;



public class GetAllAdsQuery : IRequest<PagedResult<AdDto>>
{
    public string? SearschPhrase { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? SortBy { get; set; }

    public bool? HasApartment { get; set; }

    public SortDirection SortDirection { get; set; }

    public int? CityId { get; set; }

    public List<int>? NeighborhoodIds { get; set; }
}
