using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CleanArch.Base.Template.Application.Common.Interfaces.HealthCheck;

public interface IHealthChecker : IHealthCheck
{
    public string Name { get; }
}
