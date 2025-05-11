using MediatR;
using Saknoo.Application.Lookup.Dtos;
using Saknoo.Domain.Repositories;


namespace Saknoo.Application.Lookup.Queries.GetCitiesQuery
{
    public class GetCitiesQueryHandler(ILookupRepository lookupRepository) : IRequestHandler<GetCitiesQuery, List<CityDto>>
    {


        public async Task<List<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await lookupRepository.GetAllCitiesAsync();


            var cityDtos = cities.Select(city => new CityDto
            {
                Id = city.Id,
                Name = city.Name
            }).ToList();

            return cityDtos;
        }
    }
}
