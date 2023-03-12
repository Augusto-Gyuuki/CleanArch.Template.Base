using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Base.Template.Web;

[ExcludeFromCodeCoverage]
public static class DependecyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection service)
    {
        return service;
    }
}
