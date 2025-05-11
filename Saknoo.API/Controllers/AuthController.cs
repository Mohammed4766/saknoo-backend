using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saknoo.Application.User._RefreshToken;
using Saknoo.Application.User.LoginUser;
using Saknoo.Application.User.RegisterUser;

namespace Saknoo.API.Controllers;

/// <summary>
/// Handles user authentication operations: Register, Login, and Token Refresh.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="command">Registration details (username, password, etc.).</param>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await mediator.Send(command);

        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result.Errors);
    }

    /// <summary>
    /// Authenticates a user and returns access and refresh tokens.
    /// </summary>
    /// <param name="command">User credentials (username and password).</param>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await mediator.Send(command);

        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result.Errors);
    }

    /// <summary>
    /// Refreshes JWT access token using a valid refresh token.
    /// </summary>
    /// <param name="command">Contains the refresh token and user info.</param>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        var result = await mediator.Send(command);

        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result.Errors);
    }
}
