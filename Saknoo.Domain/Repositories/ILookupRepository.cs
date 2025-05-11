using System;
using Saknoo.Domain.Entities;

namespace Saknoo.Domain.Repositories;

public interface ILookupRepository
{
    Task<List<City>> GetAllCitiesAsync();

    Task<List<Neighborhood>> GetAllNeighborhoodsAsync(int? cityId = null);

    Task<List<Nationality>> GetAllNationalitiesAsync();
}
