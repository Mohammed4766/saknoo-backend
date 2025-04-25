using System;

namespace Saknoo.Domain.Entities;

public class Nationality
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<ApplicationUser> Users { get; set; } = [];
}
