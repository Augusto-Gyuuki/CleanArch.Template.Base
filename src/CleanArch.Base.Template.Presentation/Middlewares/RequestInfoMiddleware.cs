using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Diagnostics;
using ILogger = Serilog.ILogger;

namespace CleanArch.Base.Template.Presentation.Middlewares;

public sealed class RequestTimeInfoMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public RequestTimeInfoMiddleware(ILogger logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
        LogContext.PushProperty("TraceId", traceId);

        long start = Stopwatch.GetTimestamp();

        using var reader = new StreamReader(context.Request.Body);
        var requestBody = await reader.ReadToEndAsync();

        var endpoint = context.GetEndpoint();
        var queryParams = context.Request.Query;
        var headers = context.Request.Headers;
        var path = context.Request.Path;

        var msgTemplate = $@"{{@Http}};
                            RequestBody: {{@RequestBody}}
                            QueryParams: {{@QueryParams}}
                            Headers: {{@Headers}}
                            Path: {{@Path}}";

        _logger.Information(msgTemplate,
                            endpoint?.DisplayName,
                            requestBody,
                            queryParams,
                            headers,
                            path);

        await _next(context);

        double elapsedMilliseconds = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());
        int statusCode = context.Response.StatusCode;

        _logger.Information("Endpoint {Endpoint} Responded with status: {StatusCode} in : {ElapsedMilliseconds}ms", endpoint?.DisplayName, statusCode, elapsedMilliseconds);
    }

    private static double GetElapsedMilliseconds(long start, long stop)
    {
        return (stop - start) * 1000 / (double)Stopwatch.Frequency;
    }
}

public static class RequestTimeInfoMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestTimeInfo(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestTimeInfoMiddleware>();
    }
}
