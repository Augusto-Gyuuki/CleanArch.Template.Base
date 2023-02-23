using CleanArch.Base.Template.Presentation.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Base.Template.Presentation.Common.Errors;

[ExcludeFromCodeCoverage]
public static class ApiProblemDetailsFactory
{
    private static readonly Dictionary<int, Dictionary<string, string>> errorDictionary = new()
    {
        { 400, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc7231#section-6.5.1" }, { "Title", "Bad Request" } } },
        { 401, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc7235#section-3.1" }, { "Title", "Unauthorized" } } },
        { 403, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc7231#section-6.5.3" }, { "Title", "Forbidden" } } },
        { 404, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc7231#section-6.5.4" }, { "Title", "Not Found" } } },
        { 405, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc7231#section-6.5.5" }, { "Title", "Method Not Allowed" } } },
        { 406, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc7231#section-6.5.6" }, { "Title", "Not Acceptable" } } },
        { 409, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc7231#section-6.5.8" }, { "Title", "Conflict" } } },
        { 415, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc7231#section-6.5.13" }, { "Title", "Unsupported Media Type" } } },
        { 422, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc4918#section-11.2" }, { "Title", "Unprocessable Entity" } } },
        { 500, new Dictionary<string, string>() { { "Link", "https://tools.ietf.org/html/rfc7231#section-6.6.1" }, { "Title", "An error occurred while processing your request." } } }
    };
    private const int VALIDATION_ERROR_STATUS_CODE = 400;
    private const int INTERNAL_ERROR_STATUS_CODE = 500;

    public static ProblemDetails CreateProblemDetails(HttpContext httpContext, string title, int? statusCode = null)
    {
        statusCode ??= INTERNAL_ERROR_STATUS_CODE;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);
        return problemDetails;
    }

    public static ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary)
    {
        if (modelStateDictionary is null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = VALIDATION_ERROR_STATUS_CODE,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, VALIDATION_ERROR_STATUS_CODE);

        return problemDetails;
    }

    private static void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;
        if (errorDictionary.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Skip(1).First().Value;
            problemDetails.Type ??= clientErrorData.First().Value;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId is not null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        var errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;

        if (errors is not null)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
        }
    }
}
