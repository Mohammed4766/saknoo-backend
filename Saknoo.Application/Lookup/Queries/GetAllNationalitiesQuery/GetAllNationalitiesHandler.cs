using MediatR;
using Saknoo.Application.Lookup.Dtos;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Repositories;



namespace Saknoo.Application.Lookup.Queries.GetAllNationalitiesQuery;

public class GetAllNationalitiesHandler(ILookupRepository lookupRepository ) : IRequestHandler<GetAllNationalitiesQuery, List<NationalityDto>>
{


public async Task<List<NationalityDto>> Handle(GetAllNationalitiesQuery request, CancellationToken cancellationToken)
{
    var nationalities = await lookupRepository.GetAllNationalitiesAsync();

    return nationalities.Select(n => new NationalityDto
    {
        Id = n.Id,
        Name = n.Name
    }).ToList();
}

}

