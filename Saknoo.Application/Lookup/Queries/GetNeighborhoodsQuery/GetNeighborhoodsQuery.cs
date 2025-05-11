using MediatR;
using Saknoo.Application.Lookup.Dtos;

namespace Saknoo.Application.Lookup.Queries.GetNeighborhoodsQuery;

public class GetNeighborhoodsQuery : IRequest<List<NeighborhoodDto>>
{
    public int? CityId { get; set; }
}
