using Serilog.Events;

namespace CleanArch.Base.Template.Infrastructure.Options;

public sealed class LoggerOptions
{
    public string ApplicationName { get; init; } = string.Empty;

    public string SeqUrl { get; init; } = string.Empty;

    public string SeqApiKey { get; init; } = string.Empty;

    public LogEventLevel LogEventLevel { get; init; }
}
