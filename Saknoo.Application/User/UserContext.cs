using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Saknoo.Application.User;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user == null)
        {
            throw new InvalidOperationException("User is not present in the context");
        }
        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }


        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var UserName = user.FindFirst(ClaimTypes.Name)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

        return new CurrentUser(userId, UserName, roles);
    }
}
