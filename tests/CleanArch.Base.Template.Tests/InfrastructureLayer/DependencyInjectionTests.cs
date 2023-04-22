using CleanArch.Base.Template.Application.Common.Interfaces.Authentication;
using CleanArch.Base.Template.Infrastructure;
using CleanArch.Base.Template.Infrastructure.Settings.Authentication;
using CleanArch.Base.Template.Tests.Unit.Common.Fixtures;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Serilog;

namespace CleanArch.Base.Template.Tests.Unit.InfrastructureLayer;

[Collection(nameof(InfrastructureSettingsFixture))]
public sealed class DependencyInjectionTests
{
    private readonly InfrastructureSettingsFixture _fixture;

    public DependencyInjectionTests(InfrastructureSettingsFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = "AddInfrastructure() should add Logger service to container")]
    [Trait("Infrastructure", "AddInfrastructure - Logger")]
    public void AddInfrastructure_ShouldAddLoggerToContainer()
    {
        // Arrange
        var services = DependencyInjectionFixture.GetServiceCollection();
        var infrastructureOptions = _fixture.GetSettings();

        // Act
        services.AddInfrastructure(infrastructureOptions);

        // Assert
        services.Should().NotBeNull();
        services.Any(x => x.ServiceType == typeof(ILogger)).Should().BeTrue();
    }

    [Fact(DisplayName = "AddInfrastructure() should add Auth services to container")]
    [Trait("Infrastructure", "AddInfrastructure - Auth")]
    public void AddInfrastructure_ShouldAddAuthToContainer()
    {
        // Arrange
        var services = DependencyInjectionFixture.GetServiceCollection();
        var infrastructureOptions = _fixture.GetSettings();

        // Act
        services.AddInfrastructure(infrastructureOptions);

        // Assert
        services.Should().NotBeNull();
        services.Any(x => x.ServiceType == typeof(IJwtTokenGenerator)).Should().BeTrue();
        services.Any(x => x.ServiceType == typeof(IOptions<JwtSettings>)).Should().BeTrue();
        services.Any(x => x.ServiceType == typeof(IAuthenticationService)).Should().BeTrue();
    }

    [Fact(DisplayName = "AddInfrastructure() should add HealthCheck services to container")]
    [Trait("Infrastructure", "AddInfrastructure - HealthCheck")]
    public void AddInfrastructure_ShouldAddHealthCheckToContainer()
    {
        // Arrange
        var services = DependencyInjectionFixture.GetServiceCollection();
        var infrastructureOptions = _fixture.GetSettings();

        // Act
        services.AddInfrastructure(infrastructureOptions);

        // Assert
        services.Should().NotBeNull();
        services.Any(x => x.ServiceType == typeof(HealthCheckService)).Should().BeTrue();
    }
}
