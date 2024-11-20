using Business.Exceptions;
using FluentValidation;
using Serilog;
using System.Text.Json;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred: {Message}", ex.Message);

            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case ValidationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                statusCode = context.Response.StatusCode,
                message = ex.Message,
                timeStamp = DateTime.UtcNow.ToString()
            }));
        }
    }
}