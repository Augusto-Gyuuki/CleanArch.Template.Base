using CleanArch.Base.Template.Infrastructure.Settings;
using CleanArch.Base.Template.Infrastructure.Settings.Authentication;
using CleanArch.Base.Template.Infrastructure.Settings.Logger;
using CleanArch.Base.Template.Tests.Unit.Common.Fixtures;
using Serilog.Events;

namespace CleanArch.Base.Template.Tests.Unit.InfrastructureLayer;

[CollectionDefinition(nameof(InfrastructureSettingsFixture))]
public sealed class InfrastructureSettingsFixture : BaseFixture, ICollectionFixture<InfrastructureSettingsFixture>
{
    public InfrastructureSettings GetSettings()
    {
        return new InfrastructureSettings
        {
            JwtSettings = new JwtSettings
            {
                Secret = Faker.Random.String(128),
                Audience = Faker.Lorem.Text(),
                Issuer = Faker.Lorem.Text(),
                ExpiryMinutes = Faker.Random.Number(60),
            },
            LoggerSettings = new LoggerSettings
            {
                ApplicationName = Faker.Lorem.Text(),
                SeqUrl = Faker.Internet.Url(),
                SeqApiKey = Faker.Lorem.Text(),
                LogEventLevel = Faker.PickRandom<LogEventLevel>(),
            }
        };
    }
}