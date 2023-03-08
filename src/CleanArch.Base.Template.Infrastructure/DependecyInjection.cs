using CleanArch.Base.Template.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using ILogger = Serilog.ILogger;

namespace CleanArch.Base.Template.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, InfrastructureOptions infrastructureOptions)
    {
        service
            .AddLogger(infrastructureOptions.LoggerOptions);

        return service;
    }

    private static IServiceCollection AddLogger(this IServiceCollection service, LoggerOptions loggerOptions)
    {
        var logger = new LoggerConfiguration()
                .WriteTo.Seq(loggerOptions.SeqUrl, loggerOptions.LogEventLevel, apiKey: loggerOptions.SeqApiKey)
                .Enrich.WithMachineName()
                .Enrich.FromLogContext()
                .Enrich.WithProperty(nameof(LoggerOptions.ApplicationName), loggerOptions.ApplicationName)
                .CreateLogger();

        service.AddSingleton<ILogger>(logger);
        return service;
    }
}
