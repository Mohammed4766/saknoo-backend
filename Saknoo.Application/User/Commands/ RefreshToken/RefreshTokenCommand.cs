using MediatR;
using Saknoo.Application.User.Dtos;

namespace Saknoo.Application.User._RefreshToken;

public class RefreshTokenCommand : IRequest<AuthResultDto>
{
    public string RefreshToken { get; set; } = string.Empty;
}
