using System;
using AutoMapper;
using MediatR;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Ads.Queries.GetAdByIdQuery;

public class GetAdByIdHandler(IAdRepository adRepository, IMapper mapper) : IRequestHandler<GetAdByIdQuery, AdDto>
{
    public async Task<AdDto> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
    {
        var ad = await adRepository.GetByIdAsync(request.AdId);
        if (ad is null)
        {
            throw new Exception("Ad not found");
        }
        var adDto = mapper.Map<AdDto>(ad);

        return adDto;
    }
}
