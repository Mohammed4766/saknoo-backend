using System;

namespace Saknoo.Domain.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message)
        : base(message)
    {
    }
}
