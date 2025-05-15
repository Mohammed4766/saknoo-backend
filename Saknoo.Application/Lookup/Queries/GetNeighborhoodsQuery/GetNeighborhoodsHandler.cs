using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Application.Lookup.Dtos;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Lookup.Queries.GetNeighborhoodsQuery;

public class GetNeighborhoodsHandler(
    ILookupRepository lookupRepository,
    ILogger<GetNeighborhoodsHandler> logger
) : IRequestHandler<GetNeighborhoodsQuery, List<NeighborhoodDto>>
{
    public async Task<List<NeighborhoodDto>> Handle(GetNeighborhoodsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetNeighborhoodsQuery for CityId: {CityId}", request.CityId);

        try
        {
            var neighborhoods = await lookupRepository.GetAllNeighborhoodsAsync(request.CityId);

            var result = neighborhoods.Select(n => new NeighborhoodDto
            {
                Id = n.Id,
                Name = n.Name,
                CityId = n.CityId
            }).ToList();

            logger.LogInformation("Successfully retrieved {Count} neighborhoods for CityId: {CityId}", result.Count, request.CityId);

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving neighborhoods for CityId: {CityId}", request.CityId);
            throw;
        }
    }
}
