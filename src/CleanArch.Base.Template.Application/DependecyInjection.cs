using CleanArch.Base.Template.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CleanArch.Base.Template.Application;

[ExcludeFromCodeCoverage]
public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(typeof(DependecyInjection).Assembly);

        service.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        service.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return service;
    }
}
