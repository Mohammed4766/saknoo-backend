using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saknoo.Application.User._RefreshToken;
using Saknoo.Application.User.LoginUser;
using Saknoo.Application.User.RegisterUser;

namespace Saknoo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await mediator.Send(command);
        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result.Errors);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        
        var result = await mediator.Send(command);
        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result.Errors);
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        var result = await mediator.Send(command);
        if (result.Succeeded)
            return Ok(result);

        return BadRequest(result.Errors);
    }
}
