using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saknoo.Application.Lookup.Queries.GetAllNationalitiesQuery;
using Saknoo.Application.Lookup.Queries.GetCitiesQuery;
using Saknoo.Application.Lookup.Queries.GetNeighborhoodsQuery;

namespace Saknoo.API.Controllers;

/// <summary>
/// Provides lookup data such as cities, neighborhoods, and nationalities.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LookupController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves all available cities.
    /// </summary>
    [HttpGet("cities")]
    public async Task<IActionResult> GetCities()
    {
        var result = await mediator.Send(new GetCitiesQuery());
        return Ok(result);
    }

    /// <summary>
    /// Retrieves neighborhoods for a specific city.
    /// </summary>
    /// <param name="cityId">The ID of the city to retrieve neighborhoods for.</param>
    [HttpGet("cities/{cityId}/neighborhoods")]
    public async Task<IActionResult> GetNeighborhoods([FromRoute] int cityId)
    {
        var query = new GetNeighborhoodsQuery { CityId = cityId };
        var result = await mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a list of all nationalities.
    /// </summary>
    [HttpGet("nationalities")]
    public async Task<IActionResult> GetNationalities()
    {
        var result = await mediator.Send(new GetAllNationalitiesQuery());
        return Ok(result);
    }
}
