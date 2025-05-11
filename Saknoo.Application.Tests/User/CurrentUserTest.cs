using FluentAssertions;
using Saknoo.Application.User;
using Saknoo.Domain.Constants;
using Xunit;

namespace Saknoo.Application.Tests.User;

public class CurrentUserTest
{
    [Fact]
    public void IsRole_Should_Return_True_If_User_Has_The_Role()
    {
        // Arrange
        var user = new CurrentUser(
            UserId: "1",
            UserName: "0506984766",
            Roles: new[] {UserRoles.Admin , UserRoles.User }
        );

        // Act
        var result = user.IsRole("Admin");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsRole_Should_Return_False_If_User_Does_Not_Have_The_Role()
    {
        // Arrange
        var user = new CurrentUser(
            UserId: "user123",
            UserName: "TestUser",
            Roles: new[] { UserRoles.User }
        );

        // Act
        var result = user.IsRole(UserRoles.Admin);

        // Assert
        result.Should().BeFalse();
    }
}
