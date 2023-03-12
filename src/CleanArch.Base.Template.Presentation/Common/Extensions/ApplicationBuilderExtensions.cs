using CleanArch.Base.Template.Presentation.Common.Errors;
using ErrorOr;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace CleanArch.Base.Template.Presentation.Common.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errApp =>
        {
            errApp.Run(async ctx =>
            {
                var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                if (exHandlerFeature is not null)
                {
                    var logger = ctx.Resolve<ILogger>();

                    var http = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
                    var exceptionType = exHandlerFeature.Error.GetType().Name;
                    var errorMessage = exHandlerFeature.Error.Message;
                    var queryParams = ctx.Request.Query;
                    var headers = ctx.Request.Headers;
                    var path = ctx.Request.Path;
                    using var reader = new StreamReader(ctx.Request.Body);
                    var requestBody = await reader.ReadToEndAsync();
                    var problemDetails = ApiErrorHandler.Problem(Error.Unexpected(), ctx);

                    var msgTemplate = $@"{{@Http}};
                                        ExceptionType: {{@ExceptionType}};
                                        ErrorMessage: {{@ErrorMessage}};
                                        ErrorDetails: {{@ErrorDetails}}
                                        RequestBody: {{@RequestBody}}
                                        QueryParams: {{@QueryParams}}
                                        Headers: {{@Headers}}
                                        Path: {{@Path}}";

                    logger.Error(msgTemplate,
                        http,
                        exceptionType,
                        errorMessage,
                        exHandlerFeature.Error,
                        requestBody,
                        queryParams,
                        headers,
                        path);

                    ctx.Response.StatusCode = problemDetails.Status.Value;
                    ctx.Response.ContentType = "application/problem+json";
                    await ctx.Response.WriteAsJsonAsync(problemDetails);
                }
            });
        });

        return app;
    }
}
