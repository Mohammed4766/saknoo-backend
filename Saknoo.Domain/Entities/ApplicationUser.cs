using System;
using Microsoft.AspNetCore.Identity;
using Saknoo.Domain.Constants;

namespace Saknoo.Domain.Entities;

public class ApplicationUser : IdentityUser
{

    public override string? Email { get; set; }
    public override string? UserName { get; set; }
    public string FullName { get; set; } = null!;
    public Gender Gender { get; set; }
    public int NationalityId { get; set; }
    public Nationality Nationality { get; set; } = null!;
    public ICollection<Ad> Ads { get; set; } = new List<Ad>();
     public ICollection<MatchingAnswer> Answers { get; set; } = new List<MatchingAnswer>(); // Add this line to define the relationship
}
