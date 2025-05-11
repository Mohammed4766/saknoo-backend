using AutoMapper;
using Saknoo.Application.Ads.Commands.UpdateAdCommand;
using Saknoo.Domain.Entities;
using Saknoo.Application.Ads.Dtos;

namespace Saknoo.Application.Ads.Mappings
{
    public class AdMappingProfile : Profile
    {
        public AdMappingProfile()
        {
            CreateMap<UpdateAdCommand, UpdateAdDto>();

            CreateMap<UpdateAdDto, Ad>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.AdNeighborhoods, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.HasApartment, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());




            CreateMap<Ad, AdDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.NeighborhoodNames, opt => opt.MapFrom(src => src.AdNeighborhoods.Select(an => an.Neighborhood.Name).ToList())).ReverseMap();; 

        }
    }
}
