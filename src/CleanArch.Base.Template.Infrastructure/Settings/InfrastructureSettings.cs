using CleanArch.Base.Template.Infrastructure.Settings.Authentication;
using CleanArch.Base.Template.Infrastructure.Settings.Logger;

namespace CleanArch.Base.Template.Infrastructure.Settings;

public sealed class InfrastructureSettings
{
    public const string SectionName = "Infrastructure";

    public LoggerSettings? LoggerSettings { get; init; }

    public JwtSettings? JwtSettings { get; init; }
}
