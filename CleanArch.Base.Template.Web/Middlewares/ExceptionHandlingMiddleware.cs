namespace CleanArch.Base.Template.Web.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private const string CONTENT_TYPE_JSON = "application/json";

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = CONTENT_TYPE_JSON;
    }

    private async Task HandleExceptionAsync(string requestBody, HttpContext context, Exception exception, string requestId)
    {
        const string METHOD_NAME = "HandleExceptionAsync";
    }
}
