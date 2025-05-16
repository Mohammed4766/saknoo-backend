using MediatR;

namespace Saknoo.Application.User.Commands.UpdateUserCommand;

public class UpdateUserCommand : IRequest<bool>
{
    public string? FullName { get; set; }
    public string? Bio { get; set; }
}

