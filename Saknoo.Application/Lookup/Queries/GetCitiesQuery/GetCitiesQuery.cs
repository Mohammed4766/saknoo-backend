using System;
using MediatR;
using Saknoo.Application.Lookup.Dtos;


namespace Saknoo.Application.Lookup.Queries.GetCitiesQuery;

public class GetCitiesQuery : IRequest<List<CityDto>>
{

}
