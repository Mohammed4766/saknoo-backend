using MediatR;
using Microsoft.AspNetCore.Http;

namespace Saknoo.Application.Ads.Commands.CreateAdCommand;

public class CreateAdCommand : IRequest<Guid>
{
    public required string Title { get; set; }
    public string? Description { get; set; }

    public int? Price { get; set; }
    public int? PriceFrom { get; set; }
    public int? PriceTo { get; set; }

    public bool HasApartment { get; set; }

    public int CityId { get; set; }

    public List<int> NeighborhoodIds { get; set; } = new();

    public List<IFormFile> Images { get; set; } = new();
}
