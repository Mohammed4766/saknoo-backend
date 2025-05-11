using AutoMapper;
using FluentAssertions;
using Saknoo.Application.Ads.Commands.UpdateAdCommand;
using Saknoo.Application.Ads.Dtos;
using Saknoo.Application.Ads.Mappings;
using Saknoo.Domain.Entities;
using Xunit;

namespace Saknoo.Application.Tests.Ads.Mappings;

public class AdMappingProfileTests
{
    private readonly IMapper _mapper;

    public AdMappingProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AdMappingProfile>());
        _mapper = config.CreateMapper();
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void Should_Map_UpdateAdCommand_To_UpdateAdDto()
    {
        var command = new UpdateAdCommand
        {
            Title = "Test Ad",
            Description = "A test ad description",
            Price = 1000,
            PriceFrom = 800,
            PriceTo = 1200,
            CityId = 1,
            ImageUrls = new List<string> { "img1.jpg", "img2.jpg" },
            NeighborhoodIds = new List<int> { 10, 20 }
        };

        var dto = _mapper.Map<UpdateAdDto>(command);

        dto.Title.Should().Be(command.Title);
        dto.Description.Should().Be(command.Description);
        dto.Price.Should().Be(command.Price);
        dto.CityId.Should().Be(command.CityId);
        dto.ImageUrls.Should().BeEquivalentTo(command.ImageUrls);
        dto.NeighborhoodIds.Should().BeEquivalentTo(command.NeighborhoodIds);
    }

    [Fact]
    public void Should_Map_Ad_To_AdDto()
    {
        var ad = new Ad
        {
            Id = Guid.NewGuid(),
            Title = "Ad title",
            Description = "Ad description",
            Price = 1000,
            HasApartment = true,
            UserId = "user123", // ✅ أضف هذا السطر
            User = new ApplicationUser { Id = "user123", UserName = "mohammed" },
            City = new City { Id = 1, Name = "Riyadh" },
            Images = new List<AdImage>
    {
        new() { ImageUrl = "img1.jpg" },
        new() { ImageUrl = "img2.jpg" }
    },
            AdNeighborhoods = new List<AdNeighborhood>
    {
        new() { Neighborhood = new Neighborhood { Name = "Al Olaya" } },
        new() { Neighborhood = new Neighborhood { Name = "Al Malaz" } }
    },
            CreatedAt = DateTime.UtcNow
        };


        var dto = _mapper.Map<AdDto>(ad);

        dto.Title.Should().Be(ad.Title);
        dto.Description.Should().Be(ad.Description);
        dto.Price.Should().Be(ad.Price);
        dto.CityName.Should().Be("Riyadh");
        dto.UserName.Should().Be("mohammed");
        dto.ImageUrls.Should().BeEquivalentTo(new[] { "img1.jpg", "img2.jpg" });
        dto.NeighborhoodNames.Should().BeEquivalentTo(new[] { "Al Olaya", "Al Malaz" });
    }
}
