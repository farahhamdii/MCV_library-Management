using System.Diagnostics;

namespace MVC_Task.Middlewares
{
    public class RequestMonitoringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestMonitoringMiddleware> _logger;

        public RequestMonitoringMiddleware(
            RequestDelegate next,
            ILogger<RequestMonitoringMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Request data
            var method = context.Request.Method;
            var path = context.Request.Path;

            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation(
                "Incoming Request: {Method} {Path}",
                method,
                path
            );

            // Go to the next middleware / controller
            await _next(context);

            // Response data
            stopwatch.Stop();

            var statusCode = context.Response.StatusCode;

            _logger.LogInformation(
                "Outgoing Response: {StatusCode} | {Method} {Path} | Took: {ElapsedMilliseconds} ms",
                statusCode,
                method,
                path,
                stopwatch.ElapsedMilliseconds
            );
        }
    }
}