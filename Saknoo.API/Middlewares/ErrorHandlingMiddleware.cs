using System;
using Saknoo.Domain.Exceptions;

namespace Saknoo.API.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment env)
{


    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var actualException = ex.InnerException ?? ex;
            logger.LogError(actualException, "Unhandled Exception");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = actualException switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new ErrorResponse
            {
                Success = false,
                Message = env.IsDevelopment()
                           ? $"{actualException.GetType().Name}: {actualException.Message} \n {actualException.StackTrace}"
                          : "An unexpected error occurred. Please try again later."
            };

            if (actualException is ValidationException validationEx)
            {
                response.Errors = env.IsDevelopment() ? validationEx.Errors : null;
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


