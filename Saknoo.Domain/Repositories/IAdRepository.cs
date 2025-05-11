using System;
using Saknoo.Domain.Constants;
using Saknoo.Domain.Entities;

namespace Saknoo.Domain.Repositories;

public interface IAdRepository
{
    Task<Ad> CreateAsync(Ad entity);

    Task<Ad?> GetByIdAsync(Guid adId);

    Task UpdateAsync(Ad entity);

    Task DeleteAsync(Ad entity);

    Task<(List<Ad>,int)> GetAllMatchingAsync(string? searchPhrase , int pageNumber, int pageSize , string? sortBy, SortDirection? sortDirection ,int? cityId,List<int>? neighborhoodIds , bool? HasApartment);

    Task<(List<Ad>, int)> GetAdsByUserIdAsync(string userId, int pageNumber, int pageSize);
}