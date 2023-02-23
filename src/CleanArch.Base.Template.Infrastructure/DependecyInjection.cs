using CleanArch.Base.Template.Application.Common.Interfaces.Providers;
using CleanArch.Base.Template.Infrastructure.Providers;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using ILogger = Serilog.ILogger;

namespace CleanArch.Base.Template.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        service
            .AddLogger();

        service.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return service;
    }

    private static IServiceCollection AddLogger(this IServiceCollection service)
    {
        service.AddSingleton<ILogger>(new LoggerConfiguration()
                .WriteTo.Seq("http://seq:5341")
                .Enrich.WithCorrelationId()
                .CreateLogger());

        return service;
    }
}
