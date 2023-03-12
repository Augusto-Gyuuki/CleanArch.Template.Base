using FluentValidation;

namespace CleanArch.Base.Template.Infrastructure.Settings.Logger;

public sealed class LoggerSettingsValidator : AbstractValidator<LoggerSettings>
{
    public LoggerSettingsValidator()
    {
        RuleFor(x => x.ApplicationName)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.SeqUrl)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.SeqApiKey)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.LogEventLevel)
            .IsInEnum()
            .NotEmpty()
            .NotNull();
    }
}
