namespace Saknoo.Application.Ads.Dtos;

public class AdDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int? Price { get; set; }
    public int? PriceFrom { get; set; }
    public int? PriceTo { get; set; }
    public bool HasApartment { get; set; }
    public string CityName { get; set; } = null!;
    public List<string> ImageUrls { get; set; } = new();
    public List<string> NeighborhoodNames { get; set; } = new();
    public string UserName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
