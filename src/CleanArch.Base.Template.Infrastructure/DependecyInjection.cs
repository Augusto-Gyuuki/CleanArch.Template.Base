using CleanArch.Base.Template.Application.Common.Interfaces.Providers;
using CleanArch.Base.Template.Infrastructure.Data;
using CleanArch.Base.Template.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Base.Template.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        service
            .AddDatabase()
            .AddPersistance();

        service.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return service;
    }

    private static IServiceCollection AddPersistance(this IServiceCollection service)
    {
        return service;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection service)
    {
        service.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("testDB");
        });

        return service;
    }
}
