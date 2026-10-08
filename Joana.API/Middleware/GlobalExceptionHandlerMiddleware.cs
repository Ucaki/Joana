using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Joana.API.Middleware;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = MapException(exception);

        if (statusCode == (int)HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, "Neočekivana greška prilikom obrade zahteva {Path}", context.Request.Path);
        }
        else
        {
            _logger.LogWarning(exception, "Greška prilikom obrade zahteva {Path}: {Message}", context.Request.Path, exception.Message);
        }

        var response = new
        {
            statusCode,
            message,
            timestamp = DateTimeOffset.UtcNow
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static (int StatusCode, string Message) MapException(Exception exception) => exception switch
    {
        ArgumentException => ((int)HttpStatusCode.BadRequest, exception.Message),
        KeyNotFoundException => ((int)HttpStatusCode.NotFound, exception.Message),
        UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, exception.Message),
        DbUpdateException => ((int)HttpStatusCode.Conflict, "Zapis sa unetim podacima već postoji."),
        InvalidOperationException => ((int)HttpStatusCode.UnprocessableEntity, exception.Message),
        _ => ((int)HttpStatusCode.InternalServerError, "Došlo je do neočekivane greške. Pokušajte ponovo kasnije.")
    };
}
