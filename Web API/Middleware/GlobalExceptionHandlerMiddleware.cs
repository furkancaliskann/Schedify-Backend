using Business.Exceptions;
using FluentValidation;
using System.Text.Json;
using System;

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