using System;

namespace Saknoo.Domain.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }
}

