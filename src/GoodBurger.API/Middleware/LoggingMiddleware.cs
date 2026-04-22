namespace GoodBurger.API.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var correlationId = context.Items["CorrelationId"];

        _logger.LogInformation(
            "START {Method} {Path} | CorrelationId: {CorrelationId}",
            context.Request.Method,
            context.Request.Path,
            correlationId
        );

        await _next(context);

        stopwatch.Stop();

        _logger.LogInformation(
            "END {Method} {Path} | Status: {StatusCode} | {Elapsed}ms | CorrelationId: {CorrelationId}",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds,
            correlationId
        );
    }
}
