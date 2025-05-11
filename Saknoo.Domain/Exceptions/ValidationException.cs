using System;

namespace Saknoo.Domain.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<string> Errors { get; }

    public ValidationException(IEnumerable<string> errors)
        : base("Validation failed.")
    {
        Errors = errors;
    }

    public ValidationException(string message)
        : base(message)
    {
        Errors = new List<string> { message };
    }
}

