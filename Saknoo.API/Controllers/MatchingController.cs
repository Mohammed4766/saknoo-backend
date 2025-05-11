using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Saknoo.Application.User.Commands.SubmitMatchingAnswers;
using Saknoo.Application.User.Queries.GetAllMatchingQuestions;
using Saknoo.Application.User.Queries.GetMatchingPercentage;
using MediatR;

namespace Saknoo.Api.Controllers;

/// <summary>
/// Handles operations related to user matching questions and answers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MatchingController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Submits a set of matching answers for the current user.
    /// </summary>
    /// <param name="command">Answers submitted by the user.</param>
    [HttpPost("submit-answers")]
    [Authorize]
    public async Task<IActionResult> SubmitAnswers([FromBody] SubmitMatchingAnswersCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result); 
    }

    /// <summary>
    /// Retrieves all available matching questions.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllMatchingQuestionsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Calculates the compatibility percentage between two users.
    /// </summary>
    /// <param name="userId1">First user's ID.</param>
    /// <param name="userId2">Second user's ID.</param>
    [HttpGet("matching-percentage")]
    public async Task<IActionResult> GetMatchingPercentage([FromQuery] string userId1, [FromQuery] string userId2)
    {
        var result = await mediator.Send(new GetMatchingPercentageQuery
        {
            UserId1 = userId1,
            UserId2 = userId2
        });

        return Ok(new { percentage = result });
    }
}
