using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Saknoo.Application.Ads.Commands.CreateAdCommand;
using Saknoo.Application.User;
using Saknoo.Domain.Constants;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Interfaces;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.Tests.Ads.Commands;

public class CreateAdCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateAdSuccessfully_WhenUserIsAuthenticated()
    {
        var currentUser = new CurrentUser(
            UserId: "1",
            UserName: "0506984766",
            Roles: new[] { UserRoles.User }
        );

        var fakeImages = new List<IFormFile>
        {
            CreateFakeFormFile("image1.jpg"),
            CreateFakeFormFile("image2.jpg")
        };

        var command = new CreateAdCommand
        {
            Title = "Test Ad",
            Description = "Test Description",
            Price = 1000,
            PriceFrom = 500,
            PriceTo = 1500,
            HasApartment = true,
            CityId = 1,
            NeighborhoodIds = new List<int> { 1, 2 },
            Images = fakeImages
        };

        var mockUserContext = new Mock<IUserContext>();
        mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);

        var mockAdRepository = new Mock<IAdRepository>();
        mockAdRepository.Setup(r => r.CreateAsync(It.IsAny<Ad>()))
     .ReturnsAsync((Ad ad) =>
     {
         ad.Id = Guid.NewGuid();
         return ad;
     });

        var mockBlobService = new Mock<IBlobStorageService>();
        mockBlobService.Setup(x => x.UploadToBlobAsync(It.IsAny<Stream>(), It.IsAny<string>()))
            .ReturnsAsync((Stream _, string fileName) => $"https://fake.blob.core.windows.net/{fileName}");

        var mockLogger = new Mock<ILogger<CreateAdCommandHandler>>();

        var handler = new CreateAdCommandHandler(
            adRepository: mockAdRepository.Object,
            userContext: mockUserContext.Object,
            blobStorageService: mockBlobService.Object,
            logger: mockLogger.Object
        );

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();

        mockAdRepository.Verify(r => r.CreateAsync(It.Is<Ad>(a =>
            a.Title == command.Title &&
            a.UserId == currentUser.UserId &&
            a.Description == command.Description &&
            a.Price == command.Price &&
            a.HasApartment == command.HasApartment &&
            a.CityId == command.CityId &&
            a.Images.Count == 2 &&
            a.AdNeighborhoods.Count == 2
        )), Times.Once);
    }

    private IFormFile CreateFakeFormFile(string fileName)
    {
        var content = "Fake file content";
        var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        return new FormFile(stream, 0, stream.Length, "file", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };
    }
}
