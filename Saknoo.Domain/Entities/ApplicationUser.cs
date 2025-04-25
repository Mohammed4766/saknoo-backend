using System;
using Microsoft.AspNetCore.Identity;

namespace Saknoo.Domain.Entities;

public class ApplicationUser : IdentityUser
{

    public override string? Email { get; set; }
    public override string? UserName { get; set; }
    public int NationalityId { get; set; }
    public required Nationality Nationality { get; set; }
    public ICollection<Ad> Ads { get; set; } = new List<Ad>();
}
