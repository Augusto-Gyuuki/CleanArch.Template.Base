using NetArchTest.Rules;

namespace CleanArch.Base.Template.Tests.Unit;

public sealed class ArchitectureTests
{
    private const string DOMAIN_NAMESPACE = "Domain";
    private const string APPLICATION_NAMESPACE = "Application";
    private const string INFRASTRUCTURE_NAMESPACE = "Infrastructure";
    private const string PRESENTATION_NAMESPACE = "Presentation";
    private const string PERSISTENCE_NAMESPACE = "Persistence";
    private const string WEB_NAMESPACE = "Web";

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

    //[Fact(DisplayName = "Application Layer Should Have Dependency on Domain Project")]
    //[Trait("Architecture", "Application")]
    //public void Application_ShouldHaveDependencyOnDomainProject()
    //{
    //    //Arrange
    //    var assembly = typeof(Application.AssemblyReference).Assembly;

    //    //Act
    //    var result = Types
    //        .InAssembly(assembly)
    //        .Should()
    //        .HaveDependencyOn(DOMAIN_NAMESPACE)
    //        .GetResult();

    //    //Asserts
    //    result.IsSuccessful.Should().BeTrue();
    //}

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
}