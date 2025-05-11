using System;
using Saknoo.Domain.Exceptions;

namespace Saknoo.API.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled Exception");

            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                Success = false,
                Message = ex.Message
            };

            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            if (ex is ValidationException validationEx)
            {
                response.Errors = validationEx.Errors;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

public class ErrorResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}


