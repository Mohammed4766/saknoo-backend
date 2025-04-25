using System;

namespace Saknoo.Domain.Entities;

public class Neighborhood
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CityId { get; set; }
    public required City City { get; set; }
    public List<AdNeighborhood> AdNeighborhoods { get; set; } = new();

}

