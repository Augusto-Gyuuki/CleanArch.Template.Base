using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Base.Template.Persistence;

[ExcludeFromCodeCoverage]
public static class DependecyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection service)
    {
        return service;
    }
}