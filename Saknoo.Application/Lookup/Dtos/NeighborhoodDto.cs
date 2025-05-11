using System;

namespace Saknoo.Application.Lookup.Dtos;

public class NeighborhoodDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CityId { get; set; } 
}
