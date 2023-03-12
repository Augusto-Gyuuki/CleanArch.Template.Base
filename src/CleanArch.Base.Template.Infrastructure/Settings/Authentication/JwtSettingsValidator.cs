using FluentValidation;

namespace CleanArch.Base.Template.Infrastructure.Settings.Authentication;

public sealed class JwtSettingsValidator : AbstractValidator<JwtSettings>
{
    public JwtSettingsValidator()
    {
        RuleFor(x => x.Secret)
            .NotEmpty()
            .NotNull()
            .Length(16);

        RuleFor(x => x.ExpiryMinutes)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(JwtSettings.Minimum_Expiry_Time);

        RuleFor(x => x.Issuer)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Audience)
            .NotEmpty()
            .NotNull();
    }
}
