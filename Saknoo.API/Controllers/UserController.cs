using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saknoo.Application.User.Commands.UpdateUserCommand;

namespace Saknoo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        var result = await mediator.Send(command);
        if (!result)
            return BadRequest("Failed to update user");

        return Ok("User updated successfully");
    }
}
