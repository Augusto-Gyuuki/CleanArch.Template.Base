using CleanArch.Base.Template.Web.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Base.Template.Web;

[ExcludeFromCodeCoverage]
public static class DependecyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection service)
    {
        service.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();

        var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
        service
            .AddControllers()
            .AddApplicationPart(presentationAssembly);

        return service;
    }
}
