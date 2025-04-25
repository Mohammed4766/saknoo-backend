using System;
using Microsoft.AspNetCore.Identity;
using Saknoo.Domain.Constants;

namespace Saknoo.Infrastructure.Data.Seed;

public class ApplicationDbContextSeed(ApplicationDbContext dbContext) : IApplicationDbContextSeed
{
    public async Task Seed()
    {

        if (!dbContext.Roles.Any())
        {
            var roles = GetRoles();
            dbContext.Roles.AddRange(roles);
            await dbContext.SaveChangesAsync();
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = [
            new(UserRoles.User) {
                NormalizedName = UserRoles.User.ToUpper()
            },
            new(UserRoles.Admin){
                NormalizedName = UserRoles.Admin.ToUpper()
            },
        ];

        return roles;
    }
}
