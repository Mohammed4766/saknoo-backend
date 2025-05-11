using MediatR;
using Saknoo.Application.Ads.Dtos;

namespace Saknoo.Application.Ads.Queries.GetAdByIdQuery;

public class GetAdByIdQuery(Guid AdId) : IRequest<AdDto>
{
    public Guid AdId = AdId;
}

