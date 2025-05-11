using MediatR;
using Microsoft.AspNetCore.JsonPatch;



namespace Saknoo.Application.Ads.Commands.UpdateAdCommand;

public class UpdateAdCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }
    public int? PriceFrom { get; set; }
    public int? PriceTo { get; set; }
    public int? CityId { get; set; }
    public List<string>? ImageUrls { get; set; }
    public List<int>? NeighborhoodIds { get; set; }
}
