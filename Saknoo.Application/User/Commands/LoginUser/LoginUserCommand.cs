using System;
using MediatR;
using Saknoo.Application.User.Dtos;

namespace Saknoo.Application.User.LoginUser;

public class LoginUserCommand : IRequest<AuthResultDto>
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
