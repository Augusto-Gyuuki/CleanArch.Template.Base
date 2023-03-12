namespace CleanArch.Base.Template.Infrastructure.Settings.Authentication;

public sealed class JwtSettings
{
    public const int Minimum_Expiry_Time = 60;

    public string Secret { get; init; } = string.Empty;

    public int ExpiryMinutes { get; init; }

    public string Issuer { get; init; } = string.Empty;

    public string Audience { get; init; } = string.Empty;
}
