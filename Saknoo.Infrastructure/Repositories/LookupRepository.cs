

using Microsoft.EntityFrameworkCore;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Repositories;
using Saknoo.Infrastructure.Data;

namespace Saknoo.Infrastructure.Repositories;

public class LookupRepository(ApplicationDbContext context) : ILookupRepository
{


    public async Task<List<City>> GetAllCitiesAsync()
    {
        return await context.Cities.ToListAsync();
    }

    public async Task<List<Nationality>> GetAllNationalitiesAsync()
    {
        return await context.Nationalities.ToListAsync();
    }


    public Task<List<Neighborhood>> GetAllNeighborhoodsAsync(int? cityId = null)
    {
        var query = context.Neighborhoods.AsQueryable();

        if (cityId.HasValue)
            query = query.Where(n => n.CityId == cityId);

        return query.ToListAsync();
    }
}
