// using FluentAssertions;
// using Moq;
// using Saknoo.Application.Ads.Commands.CreateAdCommand;
// using Saknoo.Application.User;
// using Saknoo.Domain.Constants;
// using Saknoo.Domain.Entities;
// using Saknoo.Domain.Repositories;

// namespace Saknoo.Application.Tests.Ads.Commands;
// public class CreateAdCommandHandlerTests
// {
//     [Fact]
//     public async Task Handle_ShouldCreateAdSuccessfully_WhenUserIsAuthenticated()
//     {
//         // Arrange
//         var currentUser = new CurrentUser(
//             UserId: "1",
//             UserName: "0506984766",
//             Roles: new[] { UserRoles.Admin, UserRoles.User }
//         );

//         var command = new CreateAdCommand
//         {
//             Title = "Test Ad",
//             Description = "Test Description",
//             Price = 1000,
//             PriceFrom = 500,
//             PriceTo = 1500,
//             HasApartment = true,
//             CityId = 1,
//             NeighborhoodIds = new List<int> { 1, 2 },
//             ImageUrls = new List<string> { "image1.jpg", "image2.jpg" }
//         };

//         var mockUserContext = new Mock<IUserContext>();
//         mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);

//         var mockAdRepository = new Mock<IAdRepository>();
//         var newAdGuid = Guid.NewGuid();
//         mockAdRepository.Setup(r => r.CreateAsync(It.IsAny<Ad>())).ReturnsAsync(new Ad
//         {
//             Id = newAdGuid, // Return the Ad with a valid Guid
//             UserId = currentUser.UserId,
//             Title = command.Title,
//             Description = command.Description,
//             Price = command.Price,
//             PriceFrom = command.PriceFrom,
//             PriceTo = command.PriceTo,
//             HasApartment = command.HasApartment,
//             CityId = command.CityId,
//             AdNeighborhoods = command.NeighborhoodIds.Select(id => new AdNeighborhood
//             {
//                 NeighborhoodId = id
//             }).ToList(),
//             Images = command.ImageUrls.Select(url => new AdImage
//             {
//                 ImageUrl = url
//             }).ToList()
//         });

//         var handler = new CreateAdCommandHandler(
//             adRepository: mockAdRepository.Object,
//             userContext: mockUserContext.Object
//         );

//         // Act
//         var result = await handler.Handle(command, CancellationToken.None);

//         // Assert
//         result.Should().NotBe(Guid.Empty); // Ensure that a valid GUID is returned

//         // Verify that CreateAsync was called with the correct Ad properties
//         mockAdRepository.Verify(r => r.CreateAsync(It.Is<Ad>(a =>
//             a.Title == command.Title &&
//             a.UserId == currentUser.UserId &&
//             a.Description == command.Description &&
//             a.Price == command.Price &&
//             a.HasApartment == command.HasApartment &&
//             a.CityId == command.CityId &&
//             a.Images.Count == 2 &&
//             a.AdNeighborhoods.Count == 2
//         )), Times.Once);
//     }




// }
