using MediatR;
using Saknoo.Application.Lookup.Dtos;
using Saknoo.Domain.Repositories;


namespace Saknoo.Application.Lookup.Queries.GetNeighborhoodsQuery;

public class GetNeighborhoodsHandler(ILookupRepository lookupRepository) : IRequestHandler<GetNeighborhoodsQuery, List<NeighborhoodDto>>
{


    public async Task<List<NeighborhoodDto>> Handle(GetNeighborhoodsQuery request, CancellationToken cancellationToken)
    {
        var neighborhoods = await lookupRepository.GetAllNeighborhoodsAsync(request.CityId);

        return neighborhoods.Select(n => new NeighborhoodDto
        {
            Id = n.Id,
            Name = n.Name,
            CityId = n.CityId
        }).ToList();
    }
}
