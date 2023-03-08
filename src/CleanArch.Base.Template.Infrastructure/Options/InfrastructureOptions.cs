namespace CleanArch.Base.Template.Infrastructure.Options;

public sealed class InfrastructureOptions
{
    public const string SectionName = "Infrastructure";

    public LoggerOptions? LoggerOptions { get; init; }
}
