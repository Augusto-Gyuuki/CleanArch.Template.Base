using CleanArch.Base.Template.Application.Common.Interfaces.Authentication;
using CleanArch.Base.Template.Application.Common.Interfaces.HealthCheck;
using CleanArch.Base.Template.Infrastructure.Authentication;
using CleanArch.Base.Template.Infrastructure.Settings;
using CleanArch.Base.Template.Infrastructure.Settings.Authentication;
using CleanArch.Base.Template.Infrastructure.Settings.Logger;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using ILogger = Serilog.ILogger;

namespace CleanArch.Base.Template.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, InfrastructureSettings infrastructureOptions)
    {
        service
            .AddLogger(infrastructureOptions.LoggerSettings)
            .AddAuth(infrastructureOptions.JwtSettings)
            .AddHealthCheck();

        return service;
    }

    private static IServiceCollection AddLogger(this IServiceCollection service, LoggerSettings loggerSettings)
    {
        var logger = new LoggerConfiguration()
                .WriteTo.Seq(loggerSettings.SeqUrl, loggerSettings.LogEventLevel, apiKey: loggerSettings.SeqApiKey)
                .Enrich.WithMachineName()
                .Enrich.FromLogContext()
                .Enrich.WithProperty(nameof(LoggerSettings.ApplicationName), loggerSettings.ApplicationName)
                .CreateLogger();

        service.AddSingleton<ILogger>(logger);

        return service;
    }

    private static IServiceCollection AddHealthCheck(this IServiceCollection service)
    {
        var healthBuilder = service.AddHealthChecks();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var healthCheckType = typeof(IHealthChecker);

        var implementations = assemblies
            .SelectMany(a => a.GetExportedTypes())
            .Where(t => !t.IsInterface
                    && !t.IsAbstract
                    && healthCheckType.IsAssignableFrom(t))
            .Select(t => (IHealthChecker)Activator.CreateInstance(t))
            .ToList();

        implementations.ForEach(healtCheckImplementation =>
        {
            if (healtCheckImplementation is null)
            {
                return;
            }

            healthBuilder.AddCheck(healtCheckImplementation.Name, healtCheckImplementation);
        });

        return service;
    }

    private static IServiceCollection AddAuth(this IServiceCollection service, JwtSettings JwtSettings)
    {
        service.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        service.AddSingleton(Options.Create(JwtSettings));

        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidAudience = JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(JwtSettings.Secret)),
                };
            });

        return service;
    }
}