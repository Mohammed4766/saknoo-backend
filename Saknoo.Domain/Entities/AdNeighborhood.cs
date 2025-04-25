using System;

namespace Saknoo.Domain.Entities;

public class AdNeighborhood
{
    public Guid AdId { get; set; }
    public int NeighborhoodId { get; set; }

    public required Ad Ad { get; set; }
    public required Neighborhood Neighborhood { get; set; }
}

