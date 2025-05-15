using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Application.Lookup.Dtos;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Lookup.Queries.GetCitiesQuery
{
    public class GetCitiesQueryHandler(
        ILookupRepository lookupRepository,
        ILogger<GetCitiesQueryHandler> logger
    ) : IRequestHandler<GetCitiesQuery, List<CityDto>>
    {
        public async Task<List<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetCitiesQuery");

            try
            {
                var cities = await lookupRepository.GetAllCitiesAsync();
                var cityDtos = cities.Select(city => new CityDto
                {
                    Id = city.Id,
                    Name = city.Name
                }).ToList();

                logger.LogInformation("Successfully retrieved {Count} cities", cityDtos.Count);

                return cityDtos;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving cities");
                throw;
            }
        }
    }
}
