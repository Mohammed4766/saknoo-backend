using System;

namespace Saknoo.Domain.Entities;

public class AdNeighborhood
{
    public Guid AdId { get; set; }
    public int NeighborhoodId { get; set; }

    public  Ad Ad { get; set; } = null!;
    public  Neighborhood Neighborhood { get; set; } = null!;
}

