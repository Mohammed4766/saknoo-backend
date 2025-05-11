using System;
using MediatR;
using Saknoo.Application.Lookup.Dtos;

namespace Saknoo.Application.Lookup.Queries.GetAllNationalitiesQuery;

public class GetAllNationalitiesQuery : IRequest<List<NationalityDto>>
{

}
