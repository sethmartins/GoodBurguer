using FluentValidation;
using GoodBurger.Domain.Exceptions;
using System.Text.Json;

namespace GoodBurger.API.Middleware;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidation(context, ex);
        }
        catch (DomainException ex)
        {
            await HandleDomain(context, ex);
        }
        catch (Exception ex)
        {
            await HandleUnexpected(context, ex);
        }
    }

    private async Task HandleValidation(HttpContext context, ValidationException ex)
    {
        var correlationId = context.Items["CorrelationId"];

        _logger.LogWarning(ex,
            "VALIDATION ERROR | CorrelationId: {CorrelationId}",
            correlationId);

        context.Response.StatusCode = 400;

        var errors = ex.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage));

        await context.Response.WriteAsJsonAsync(new
        {
            type = "validation_error",
            correlationId,
            errors
        });
    }

    private async Task HandleDomain(HttpContext context, DomainException ex)
    {
        var correlationId = context.Items["CorrelationId"];

        _logger.LogWarning(ex,
            "DOMAIN ERROR | CorrelationId: {CorrelationId}",
            correlationId);

        context.Response.StatusCode = 400;

        await context.Response.WriteAsJsonAsync(new
        {
            type = "domain_error",
            correlationId,
            message = ex.Message
        });
    }

    private async Task HandleUnexpected(HttpContext context, Exception ex)
    {
        var correlationId = context.Items["CorrelationId"];

        _logger.LogError(ex,
            "UNEXPECTED ERROR | CorrelationId: {CorrelationId}",
            correlationId);

        context.Response.StatusCode = 500;

        await context.Response.WriteAsJsonAsync(new
        {
            type = "internal_error",
            correlationId,
            message = "Erro interno"
        });
    }
}