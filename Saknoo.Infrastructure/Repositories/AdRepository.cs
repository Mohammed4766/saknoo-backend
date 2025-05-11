using Saknoo.Domain.Entities;
using Saknoo.Domain.Repositories;
using Saknoo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Saknoo.Domain.Constants;
using System.Linq.Dynamic.Core;


namespace Saknoo.Infrastructure.Repositories;

public class AdRepository(ApplicationDbContext context) : IAdRepository
{


    public async Task<Ad> CreateAsync(Ad entity)
    {
        context.Ads.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Ad entity)
    {
        context.Ads.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<Ad?> GetByIdAsync(Guid adId)
    {
        return await context.Ads
            .Include(a => a.Images)
            .Include(a => a.AdNeighborhoods)
                .ThenInclude(an => an.Neighborhood)
            .Include(a => a.City)
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == adId);
    }



    public async Task UpdateAsync(Ad entity)
    {
        context.Ads.Update(entity);
        await context.SaveChangesAsync();
    }



    public async Task<(List<Ad>, int)> GetAllMatchingAsync(
        string? searchPhrase,
        int pageNumber,
        int pageSize,
        string? sortBy,
        SortDirection? sortDirection,
        int? cityId,
        List<int>? neighborhoodIds,
        bool? hasApartment)
    {
        var query = context.Ads
            .AsNoTracking()
            .Include(a => a.Images)
            .Include(a => a.City)
            .Include(a => a.AdNeighborhoods)
                .ThenInclude(an => an.Neighborhood)
            .Include(a => a.User)
            .AsQueryable();

        // Filtering
        if (!string.IsNullOrWhiteSpace(searchPhrase))
        {
            query = query.Where(a =>
                a.Title.Contains(searchPhrase) ||
                a.Description.Contains(searchPhrase));
        }

        if (cityId.HasValue)
        {
            query = query.Where(a => a.CityId == cityId);
        }

        if (neighborhoodIds != null && neighborhoodIds.Any())
        {
            query = query.Where(a =>
                a.AdNeighborhoods.Any(an => neighborhoodIds.Contains(an.NeighborhoodId)));
        }

        if (hasApartment.HasValue)
        {
            query = query.Where(a => a.HasApartment == hasApartment);
        }

        // Total count AFTER filtering
        var totalCount = await query.CountAsync();

        // Sorting
        // Sorting
        if (!string.IsNullOrEmpty(sortBy))
        {
            var direction = sortDirection == SortDirection.Ascending ? "ascending" : "descending";
            query = query.OrderBy($"{sortBy} {direction}");
        }
        else
        {
            query = query.OrderByDescending(a => a.CreatedAt); // default sorting
        }


        // Paging
        var ads = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (ads, totalCount);
    }





    public async Task<(List<Ad>, int)> GetAdsByUserIdAsync(string userId, int pageNumber, int pageSize)
    {
        var baseQuery = context.Ads
             .Include(a => a.Images)
             .Include(a => a.City)
             .Include(a => a.AdNeighborhoods)
             .ThenInclude(an => an.Neighborhood)
             .Include(a => a.User)
            .Where(a => a.UserId == userId);


        var totalCount = await baseQuery.CountAsync();

        var ads = await baseQuery
            .OrderByDescending(a => a.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (ads, totalCount);
    }


}
