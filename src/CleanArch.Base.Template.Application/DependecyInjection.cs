using CleanArch.Base.Template.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArch.Base.Template.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<AssemblyReference>();

            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Scoped);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>), ServiceLifetime.Scoped);
        });

        //service.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        //service.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return service;
    }
}
