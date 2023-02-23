using FastEndpoints;
using FastEndpoints.Swagger;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using IMapper = MapsterMapper.IMapper;

namespace CleanArch.Base.Template.Presentation;

[ExcludeFromCodeCoverage]
public static class DependecyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection service)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        service.AddSingleton(config);

        service.AddScoped<IMapper, ServiceMapper>();

        service.AddFastEndpoints(options =>
        {
            options.DisableAutoDiscovery = true;
            options.Assemblies = new[] { Assembly.GetExecutingAssembly() };
        });

        service.AddSwaggerDoc();

        return service;
    }
}