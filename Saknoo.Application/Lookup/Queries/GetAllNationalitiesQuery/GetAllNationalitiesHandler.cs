using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Application.Lookup.Dtos;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Lookup.Queries.GetAllNationalitiesQuery;

public class GetAllNationalitiesHandler(
    ILookupRepository lookupRepository,
    ILogger<GetAllNationalitiesHandler> logger
) : IRequestHandler<GetAllNationalitiesQuery, List<NationalityDto>>
{
    public async Task<List<NationalityDto>> Handle(GetAllNationalitiesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetAllNationalitiesQuery");

        try
        {
            var nationalities = await lookupRepository.GetAllNationalitiesAsync();

            logger.LogInformation("Successfully retrieved {Count} nationalities", nationalities.Count);

            return nationalities.Select(n => new NationalityDto
            {
                Id = n.Id,
                Name = n.Name
            }).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving nationalities");
            throw;
        }
    }
}
