using CleanArch.Base.Template.Application;
using CleanArch.Base.Template.Application.Common.Behaviors;
using CleanArch.Base.Template.Tests.Unit.Common.Fixtures;
using MediatR;

namespace CleanArch.Base.Template.Tests.Unit.ApplicationLayer;

public sealed class DependencyInjectionTests
{
    [Fact(DisplayName = "AddApplication() should add MediatR to container")]
    [Trait("Application", "AddApplication - MediatR")]
    public void AddApplication_ShouldAddMediatRToContainer()
    {
        // Arrange
        var services = DependencyInjectionFixture.GetServiceCollection();

        // Act
        services.AddApplication();

        // Assert
        services.Should()
            .NotBeNull();

        services.Any(x => x.ServiceType == typeof(IMediator))
            .Should()
            .BeTrue();

        services.Any(x => x.ServiceType == typeof(IPipelineBehavior<,>)
            && x.ImplementationType == typeof(ValidationBehavior<,>))
            .Should()
            .BeTrue();

        services.Any(x => x.ServiceType == typeof(IPipelineBehavior<,>)
            && x.ImplementationType == typeof(LoggingBehavior<,>))
            .Should()
            .BeTrue();
    }
}
