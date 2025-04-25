using System;
using Microsoft.AspNetCore.Identity;
using Saknoo.Domain.Constants;
using Saknoo.Domain.Entities;

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

        if (!dbContext.Nationalities.Any())
        {
            var nationalities = GetNationalities();
            dbContext.Nationalities.AddRange(nationalities);
        }

        if (!dbContext.Cities.Any())
        {
            var cities = GetCities();
            await dbContext.Cities.AddRangeAsync(cities);
            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.Neighborhoods.Any())
        {
            var neighborhoods = GetNeighborhoods();
            await dbContext.Neighborhoods.AddRangeAsync(neighborhoods);
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

    private IEnumerable<Nationality> GetNationalities()
    {
        return
    [
        new() { Name = "السعودية" },
        new() { Name = "الإمارات" },
        new() { Name = "الكويت" },
        new() { Name = "قطر" },
        new() { Name = "البحرين" },
        new() { Name = "عُمان" },
        new() { Name = "مصر" },
        new() { Name = "اليمن" },
        new() { Name = "سوريا" },
        new() { Name = "فلسطين" },
        new() { Name = "الأردن" },
    ];
    }


    private IEnumerable<City> GetCities()
    {
        return
            [
        new() { Name = "الرياض" },
        new() { Name = "الدمام" },
        new() { Name = "جدة" }
            ];
    }

    private IEnumerable<Neighborhood> GetNeighborhoods()
    {
        return new List<Neighborhood>
    {
        // Riyadh neighborhoods
        new() { Name = "الملز", CityId = 1 },
        new() { Name = "العزيزية", CityId = 1 },
        new() { Name = "الصحافة", CityId = 1 },
        new() { Name = "السويدي", CityId = 1 },
        new() { Name = "الروضة", CityId = 1 },
        new() { Name = "المرشد", CityId = 1 },
        new() { Name = "الشميسي", CityId = 1 },
        new() { Name = "المغرزات", CityId = 1 },
        new() { Name = "الطائف", CityId = 1 },
        new() { Name = "الملقا", CityId = 1 },
        new() { Name = "الحي الدبلوماسي", CityId = 1 },
        new() { Name = "النزهة", CityId = 1 },
        new() { Name = "الصحيفة", CityId = 1 },
        new() { Name = "الفاخرية", CityId = 1 },
        new() { Name = "الورود", CityId = 1 },
        new() { Name = "الربوة", CityId = 1 },
        new() { Name = "الدرعية", CityId = 1 },
        new() { Name = "المربع", CityId = 1 },
        new() { Name = "الضباط", CityId = 1 },
        new() { Name = "الجنادرية", CityId = 1 },
        new() { Name = "الخزامى", CityId = 1 },
        new() { Name = "الفيحاء", CityId = 1 },
        new() { Name = "المعذر", CityId = 1 },
        new() { Name = "المحيط", CityId = 1 },
        new() { Name = "اليرموك", CityId = 1 },
        new() { Name = "الشرق", CityId = 1 },
        new() { Name = "الحمراء", CityId = 1 },
        new() { Name = "السلام", CityId = 1 },
        new() { Name = "الملحق", CityId = 1 },
        new() { Name = "أم الحمام", CityId = 1 },
        new() { Name = "السلي", CityId = 1 },
        new() { Name = "السعودية", CityId = 1 },
        new() { Name = "البرادات", CityId = 1 },
        new() { Name = "القيصومة", CityId = 1 },
        new() { Name = "شقراء", CityId = 1 },
        new() { Name = "التخصصي", CityId = 1 },
        new() { Name = "العقيق", CityId = 1 },
        new() { Name = "الزهور", CityId = 1 },
        new() { Name = "النخيل", CityId = 1 },
        new() { Name = "العليا", CityId = 1 },
        new() { Name = "الشميسي", CityId = 1 },
        new() { Name = "حي المروج", CityId = 1 },
        new() { Name = "حي الملقا", CityId = 1 },
        new() { Name = "حي النزهة", CityId = 1 },
        new() { Name = "شمال الرياض", CityId = 1 },
        new() { Name = "شرق الرياض", CityId = 1 },
        new() { Name = "غرب الرياض", CityId = 1 },
        new() { Name = "جنوب الرياض", CityId = 1 },

        // Dammam neighborhoods
        new() { Name = "الشاطئ", CityId = 2 },
        new() { Name = "المنار", CityId = 2 },
        new() { Name = "الرفعة", CityId = 2 },
        new() { Name = "النور", CityId = 2 },
        new() { Name = "الزهور", CityId = 2 },
        new() { Name = "الشاطئ الشرقي", CityId = 2 },
        new() { Name = "الفيصلية", CityId = 2 },
        new() { Name = "الراكة", CityId = 2 },
        new() { Name = "الدمام الجديدة", CityId = 2 },
        new() { Name = "حي العدامة", CityId = 2 },
        new() { Name = "حي الجلوية", CityId = 2 },

        // Jeddah neighborhoods
        new() { Name = "الروضة", CityId = 3 },
        new() { Name = "البغدادية", CityId = 3 },
        new() { Name = "الشاطئ", CityId = 3 },
        new() { Name = "الصفا", CityId = 3 },
        new() { Name = "العزيزية", CityId = 3 },
        new() { Name = "حي الحمدانية", CityId = 3 },
        new() { Name = "حي النسيم", CityId = 3 },
        new() { Name = "حي الزهراء", CityId = 3 },
        new() { Name = "حي النعيم", CityId = 3 },
        new() { Name = "حي القريات", CityId = 3 },
        new() { Name = "حي الأندلس", CityId = 3 },
        new() { Name = "حي السلامة", CityId = 3 },
        new() { Name = "حي الحمراء", CityId = 3 },
        new() { Name = "حي السليمانية", CityId = 3 },
        new() { Name = "حي الشمال", CityId = 3 },
        new() { Name = "حي المربع", CityId = 3 },
        new() { Name = "حي البساتين", CityId = 3 },
        new() { Name = "حي المنار", CityId = 3 },
        new() { Name = "حي العزيزية", CityId = 3 },
        new() { Name = "حي الرغامة", CityId = 3 },
        new() { Name = "حي جدة التاريخية", CityId = 3 },
        new() { Name = "حي المصفاة", CityId = 3 },
        new() { Name = "حي الرحاب", CityId = 3 },
        new() { Name = "حي الشاطئ الغربي", CityId = 3 }
    };
    }

}
