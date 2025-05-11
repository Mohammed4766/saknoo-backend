namespace Saknoo.Domain.Entities;

public class Ad
{
    public Guid Id { get; set; }

    public required string UserId { get; set; }
    public  ApplicationUser User { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public int? Price { get; set; }
    public int? PriceFrom { get; set; }
    public int? PriceTo { get; set; }

    public bool HasApartment { get; set; }

    public int CityId { get; set; }
    public City City { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<AdImage> Images { get; set; } = new();
    public List<AdNeighborhood> AdNeighborhoods { get; set; } = new();

}

