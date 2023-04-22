using NetArchTest.Rules;

namespace CleanArch.Base.Template.Tests.Unit.Architecture;

public sealed class ArchitectureTests
{
    private const string DOMAIN_NAMESPACE = "CleanArch.Base.Template.Domain";
    private const string APPLICATION_NAMESPACE = "CleanArch.Base.Template.Application";
    private const string INFRASTRUCTURE_NAMESPACE = "CleanArch.Base.Template.Infrastructure";
    private const string PRESENTATION_NAMESPACE = "CleanArch.Base.Template.Presentation";
    private const string PERSISTENCE_NAMESPACE = "CleanArch.Base.Template.Persistence";
    private const string WEB_NAMESPACE = "CleanArch.Base.Template.Web";

    [Fact(DisplayName = "Domain Layer Should Not Have Dependency on Others Projects")]
    [Trait("Architecture", "Domain")]
    public void Domain_ShouldNotHaveDependencyOnOthersProjects()
    {
        //Arrange
        var assembly = typeof(Domain.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            WEB_NAMESPACE,
            PERSISTENCE_NAMESPACE,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Asserts
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact(DisplayName = "Application Layer Should Not Have Dependency on Others Projects")]
    [Trait("Architecture", "Application")]
    public void Application_ShouldNotHaveDependencyOnOthersProjects()
    {
        //Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            WEB_NAMESPACE,
            PERSISTENCE_NAMESPACE,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Asserts
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact(DisplayName = "Handlers Should Have Dependency on Domain")]
    [Trait("Architecture", "Application")]
    public void Handlers_ShouldHaveDependencyOnDomain()
    {
        //Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DOMAIN_NAMESPACE)
            .GetResult();

        //Asserts
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact(DisplayName = "Infrastructure Layer Should Not Have Dependency on Others Projects")]
    [Trait("Architecture", "Infrastructure")]
    public void Infrastructure_ShouldNotHaveDependencyOnOthersProjects()
    {
        //Arrange
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            PRESENTATION_NAMESPACE,
            WEB_NAMESPACE,
            PERSISTENCE_NAMESPACE,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Asserts
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact(DisplayName = "Persistence Layer Should Not Have Dependency on Others Projects")]
    [Trait("Architecture", "Persistence")]
    public void Persistence_ShouldNotHaveDependencyOnOthersProjects()
    {
        //Arrange
        var assembly = typeof(Persistence.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            PRESENTATION_NAMESPACE,
            WEB_NAMESPACE,
            PERSISTENCE_NAMESPACE,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Asserts
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact(DisplayName = "Presentation Layer Endpoints Should Have Dependecy On FastEndpoint")]
    [Trait("Architecture", "Presentation")]
    public void Presentation_EndpointShouldHaveDependecyOnFastEndpoint()
    {
        //Arrange
        var assembly = typeof(Presentation.AssemblyReference).Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Endpoint")
            .Should()
            .HaveDependencyOn("FastEndpoints")
            .GetResult();

        //Asserts
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact(DisplayName = "Presentation Layer Should Not Have Dependency on Others Projects")]
    [Trait("Architecture", "Presentation")]
    public void Presentation_ShouldNotHaveDependencyOnOthersProjects()
    {
        //Arrange
        var assembly = typeof(Presentation.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            INFRASTRUCTURE_NAMESPACE,
            WEB_NAMESPACE,
            PERSISTENCE_NAMESPACE,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Asserts
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact(DisplayName = "Presentation Layer Should Have Dependency on Application Project", Skip = "Only Works after developing")]
    [Trait("Architecture", "Presentation")]
    public void Presentation_ShouldHaveDependencyOnApplicationProject()
    {
        //Arrange
        var assembly = typeof(Presentation.AssemblyReference).Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Endpoint")
            .Should()
            .HaveDependencyOn(APPLICATION_NAMESPACE)
            .GetResult();

        //Asserts
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact(DisplayName = "Web Layer Should Have Dependency on Others Projects")]
    [Trait("Architecture", "Web")]
    public void Web_ShouldNotHaveDependencyOnOthersProjects()
    {
        //Arrange
        var assembly = typeof(Web.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            PRESENTATION_NAMESPACE,
            PERSISTENCE_NAMESPACE,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .HaveName("Program")
            .Should()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Asserts
        result.IsSuccessful.Should().BeTrue();
    }
}