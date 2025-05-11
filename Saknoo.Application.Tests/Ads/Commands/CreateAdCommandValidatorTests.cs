// using FluentAssertions;
// using Saknoo.Application.Ads.Commands.CreateAdCommand;
// using Xunit;

// namespace Saknoo.Application.Tests.Ads.Commands;

// public class CreateAdCommandValidatorTests
// {
//     private readonly CreateAdCommandValidator _validator = new();

//     [Fact]
//     public void Should_Pass_When_User_Has_Apartment_And_All_Valid()
//     {
//         var command = new CreateAdCommand
//         {
//             Title = "Nice place",
//             CityId = 1,
//             HasApartment = true,
//             Price = 1500,
//             Images = new List<string> { "img1.jpg" },
//             NeighborhoodIds = new List<int> { 2 }
//         };

//         var result = _validator.Validate(command);

//         result.IsValid.Should().BeTrue();
//     }

//     [Fact]
//     public void Should_Fail_When_HasApartment_Without_Price()
//     {
//         var command = new CreateAdCommand
//         {
//             Title = "Missing price",
//             CityId = 1,
//             HasApartment = true,
//             Price = null,
//             ImageUrls = new List<string> { "img1.jpg" },
//             NeighborhoodIds = new List<int> { 2 }
//         };

//         var result = _validator.Validate(command);

//         result.IsValid.Should().BeFalse();
//         result.Errors.Should().Contain(x => x.PropertyName == "Price");
//     }

//     [Fact]
//     public void Should_Fail_When_HasApartment_Without_Images()
//     {
//         var command = new CreateAdCommand
//         {
//             Title = "No images",
//             CityId = 1,
//             HasApartment = true,
//             Price = 1200,
//             ImageUrls = new List<string>(),
//             NeighborhoodIds = new List<int> { 2 }
//         };

//         var result = _validator.Validate(command);

//         result.IsValid.Should().BeFalse();
//         result.Errors.Should().Contain(x => x.PropertyName == "ImageUrls");
//     }

//     [Fact]
//     public void Should_Fail_When_HasApartment_With_No_Neighborhood()
//     {
//         var command = new CreateAdCommand
//         {
//             Title = "No neighborhood",
//             CityId = 1,
//             HasApartment = true,
//             Price = 1200,
//             ImageUrls = new List<string> { "img1.jpg" },
//             NeighborhoodIds = new List<int>() // Empty
//         };

//         var result = _validator.Validate(command);

//         result.IsValid.Should().BeFalse();
//         result.Errors.Should().Contain(x => x.PropertyName == "NeighborhoodIds");
//     }

//     [Fact]
//     public void Should_Pass_When_DoesNotHaveApartment_And_Data_Is_Valid()
//     {
//         var command = new CreateAdCommand
//         {
//             Title = "Looking for a place",
//             CityId = 1,
//             HasApartment = false,
//             Price = 0,
//             PriceFrom = 1000,
//             PriceTo = 2000,
//             NeighborhoodIds = new List<int> { 3 },
//             ImageUrls = new List<string>() // No images
//         };

//         var result = _validator.Validate(command);

//         result.IsValid.Should().BeTrue();
//     }

//     [Fact]
//     public void Should_Fail_When_DoesNotHaveApartment_With_Image()
//     {
//         var command = new CreateAdCommand
//         {
//             Title = "Has image",
//             CityId = 1,
//             HasApartment = false,
//             PriceFrom = 1000,
//             PriceTo = 2000,
//             NeighborhoodIds = new List<int> { 3 },
//             ImageUrls = new List<string> { "img1.jpg" }
//         };

//         var result = _validator.Validate(command);

//         result.IsValid.Should().BeFalse();
//         result.Errors.Should().Contain(x => x.PropertyName == "ImageUrls");
//     }

//     [Fact]
//     public void Should_Fail_When_DoesNotHaveApartment_With_Fixed_Price()
//     {
//         var command = new CreateAdCommand
//         {
//             Title = "Fixed price",
//             CityId = 1,
//             HasApartment = false,
//             Price = 2000,
//             PriceFrom = 1000,
//             PriceTo = 2000,
//             NeighborhoodIds = new List<int> { 3 }
//         };

//         var result = _validator.Validate(command);

//         result.IsValid.Should().BeFalse();
//         result.Errors.Should().Contain(x => x.PropertyName == "Price");
//     }

//     [Fact]
//     public void Should_Fail_When_DoesNotHaveApartment_Without_PriceRange()
//     {
//         var command = new CreateAdCommand
//         {
//             Title = "No price range",
//             CityId = 1,
//             HasApartment = false,
//             Price = 0,
//             PriceFrom = null,
//             PriceTo = null,
//             NeighborhoodIds = new List<int> { 3 }
//         };

//         var result = _validator.Validate(command);

//         result.IsValid.Should().BeFalse();
//         result.Errors.Should().Contain(x => x.PropertyName == "PriceFrom");
//         result.Errors.Should().Contain(x => x.PropertyName == "PriceTo");
//     }

//     [Fact]
//     public void Should_Fail_When_DoesNotHaveApartment_Without_Neighborhood()
//     {
//         var command = new CreateAdCommand
//         {
//             Title = "No neighborhood",
//             CityId = 1,
//             HasApartment = false,
//             PriceFrom = 1000,
//             PriceTo = 2000,
//             NeighborhoodIds = new List<int>() // Empty
//         };

//         var result = _validator.Validate(command);

//         result.IsValid.Should().BeFalse();
//         result.Errors.Should().Contain(x => x.PropertyName == "NeighborhoodIds");
//     }
// }
