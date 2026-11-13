using System.Net;
using System.Text.Json;

namespace SistemaPacientes.API.Middleware;

/// <summary>
/// Middleware global de manejo de errores: captura excepciones no controladas
/// y devuelve una respuesta JSON uniforme.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado: {Mensaje}", ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var payload = JsonSerializer.Serialize(new
            {
                estado = context.Response.StatusCode,
                mensaje = "Ocurrió un error interno en el servidor.",
                detalle = ex.Message
            });
            await context.Response.WriteAsync(payload);
        }
    }
}
