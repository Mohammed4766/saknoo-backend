using MediatR;
using Microsoft.AspNetCore.Identity;
using Saknoo.Application.User.Dtos;
using Saknoo.Domain.Constants;


namespace Saknoo.Application.User.RegisterUser;

public class RegisterUserCommand : IRequest<AuthResultDto>
{
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int NationalityId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
}
