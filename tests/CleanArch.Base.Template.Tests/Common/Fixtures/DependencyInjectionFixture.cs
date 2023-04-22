using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Base.Template.Tests.Unit.Common.Fixtures;

public sealed class DependencyInjectionFixture
{
    public static ServiceCollection GetServiceCollection()
    {
        return new ServiceCollection();
    }
}
