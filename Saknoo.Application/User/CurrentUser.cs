using System;

namespace Saknoo.Application.User;

public record CurrentUser(string UserId, string UserName, IEnumerable<string> Roles)
{
    public bool IsRole(string role) => Roles.Contains(role);
}

