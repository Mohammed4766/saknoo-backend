using System;

namespace Saknoo.Domain.Entities;

public class Neighborhood
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CityId { get; set; }
    public City City { get; set; } = null!;
    public List<AdNeighborhood> AdNeighborhoods { get; set; } = new();

}

