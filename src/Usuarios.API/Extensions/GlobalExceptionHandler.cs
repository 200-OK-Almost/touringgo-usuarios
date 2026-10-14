using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Domain.Exceptions;

namespace Usuarios.API.Extensions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var (statusCode, title, detail) = exception switch
        {
            NotFoundException => (
                StatusCodes.Status404NotFound,
                "No se encontró el recurso",
                exception.Message
            ),

            DominioException => (
                StatusCodes.Status400BadRequest,
                "Error de validación de regla de dominio",
                exception.Message
            ),

            _ =>
                (
                StatusCodes.Status500InternalServerError,
                "Ocurrió un error inesperado",
                (string?)null
            )
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(
                exception,
                "Ocurrió un error inesperado al procesar la solicitud."
            );
        }
        else
        {
            _logger.LogWarning(
                "La solicitud falló con {StatusCode}: {Message}",
                statusCode,
                exception.Message
            );
        }

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            problem,
            cancellationToken);

        return true;
    }
}