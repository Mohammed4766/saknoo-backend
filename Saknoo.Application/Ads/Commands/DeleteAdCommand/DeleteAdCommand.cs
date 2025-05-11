using MediatR;

namespace Saknoo.Application.Ads.Commands.DeleteAdCommand;



public class DeleteAdCommand(Guid id) : IRequest<bool>
{
    public Guid Id { get; set; } = id;
}


