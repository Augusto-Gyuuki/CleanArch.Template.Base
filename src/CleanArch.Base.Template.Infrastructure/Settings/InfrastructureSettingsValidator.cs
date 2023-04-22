using CleanArch.Base.Template.Infrastructure.Settings.Authentication;
using CleanArch.Base.Template.Infrastructure.Settings.Logger;
using FluentValidation;

namespace CleanArch.Base.Template.Infrastructure.Settings;

public sealed class InfrastructureSettingsValidator : AbstractValidator<InfrastructureSettings>
{
    public InfrastructureSettingsValidator()
    {
        RuleFor(x => x.JwtSettings)
            .SetValidator(new JwtSettingsValidator());

        RuleFor(x => x.LoggerSettings)
            .SetValidator(new LoggerSettingsValidator());
    }
}
