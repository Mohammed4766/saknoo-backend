using System;

namespace Saknoo.Domain.Entities;

public class AdImage
{
    public Guid Id { get; set; }
    public Guid AdId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public Ad Ad { get; set; } = null!;
}

