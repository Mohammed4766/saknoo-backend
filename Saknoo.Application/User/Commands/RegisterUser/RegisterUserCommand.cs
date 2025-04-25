using MediatR;
using Microsoft.AspNetCore.Identity;
using Saknoo.Application.User.Dtos;


namespace Saknoo.Application.User.RegisterUser;

public class RegisterUserCommand : IRequest<AuthResultDto>
{
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
}
