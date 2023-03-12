using CleanArch.Base.Template.Presentation.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanArch.Base.Template.Presentation.Common.Errors;

public static class ApiErrorHandler
{
    public static ProblemDetails Problem(List<Error> errors, HttpContext httpContext)
    {
        if (errors.All(x => x.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors, httpContext);
        }

        httpContext.Items[HttpContextItemKeys.Errors] = errors;

        return Problem(errors.First(), httpContext);
    }

    public static ProblemDetails Problem(Error error, HttpContext httpContext)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };

        return ApiProblemDetailsFactory.CreateProblemDetails(httpContext, error.Description, statusCode);
    }

    private static ValidationProblemDetails ValidationProblem(List<Error> errors, HttpContext httpContext)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }

        return ApiProblemDetailsFactory.CreateValidationProblemDetails(httpContext, modelStateDictionary);
    }
}
