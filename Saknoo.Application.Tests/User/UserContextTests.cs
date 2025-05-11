using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Saknoo.Application.User;
using Saknoo.Domain.Constants;

namespace Saknoo.Application.Tests.User;

public class UserContextTests
{
    [Fact]
    public void GetCurrentUser_ShouldReturnUser_WhenAuthenticated()
    {
        // Arrange
        var userId = "123";
        var userName = "0506984766";
        var roles = new[] { UserRoles.Admin, UserRoles.User };

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName),
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var identity = new ClaimsIdentity(claims, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        var userContext = new UserContext(mockHttpContextAccessor.Object);

        // Act
        var result = userContext.GetCurrentUser();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result!.UserId);
        Assert.Equal(userName, result.UserName);
        Assert.True(result.IsRole("Admin"));
        Assert.True(result.IsRole("User"));
        Assert.False(result.IsRole("Other"));
    }

    [Fact]
    public void GetCurrentUser_ShouldReturnNull_WhenNotAuthenticated()
    {
        // Arrange
        var identity = new ClaimsIdentity();
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        var userContext = new UserContext(mockHttpContextAccessor.Object);

        // Act
        var result = userContext.GetCurrentUser();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetCurrentUser_ShouldThrowException_WhenNoHttpContext()
    {
        // Arrange
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(() => null);

        var userContext = new UserContext(mockHttpContextAccessor.Object);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userContext.GetCurrentUser());
    }
}
