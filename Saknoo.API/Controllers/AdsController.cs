using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saknoo.Application.Ads.Commands.CreateAdCommand;
using Saknoo.Application.Ads.Commands.DeleteAdCommand;
using Saknoo.Application.Ads.Commands.UpdateAdCommand;
using Saknoo.Application.Ads.Queries.GetAdByIdQuery;
using Saknoo.Application.Ads.Queries.GetAllAdsQuery;
using Saknoo.Application.Ads.Queries.GetUserAdsQuery;
using Saknoo.Infrastructure.Services;

namespace Saknoo.API.Controllers;

/// <summary>
/// Handles operations related to Ads such as Create, Read, Update, and Delete (CRUD).
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AdsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a new ad.
    /// </summary>
    /// <param name="command">Command object that contains ad creation data.</param>
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateAdCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }

    /// <summary>
    /// Retrieves an ad by its ID.
    /// </summary>
    /// <param name="id">Ad unique identifier.</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetAdByIdQuery(id));
        return Ok(result);
    }

    /// <summary>
    /// Retrieves all ads with optional filters.
    /// </summary>
    /// <param name="query">Query object that may include filters, paging, etc.</param>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllAdsQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing ad.
    /// </summary>
    /// <param name="id">Ad ID to update.</param>
    /// <param name="command">Updated ad data.</param>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAd([FromRoute] Guid id, [FromBody] UpdateAdCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Deletes an ad by ID.
    /// </summary>
    /// <param name="id">Ad ID to delete.</param>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteAdCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Retrieves all ads for a specific user.
    /// </summary>
    /// <param name="userId">User ID whose ads will be returned.</param>
    /// <param name="pageNumber">Optional page number for pagination.</param>
    /// <param name="pageSize">Optional page size for pagination.</param>
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserAds([FromRoute] string userId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var query = new GetUserAdsQuery
        {
            UserId = userId,
            PageNumber = pageNumber ?? 1,
            PageSize = pageSize ?? 10,
        };

        var result = await mediator.Send(query);
        return Ok(result);
    }




}


