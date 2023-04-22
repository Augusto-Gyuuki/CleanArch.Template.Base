using Bogus;
using CleanArch.Base.Template.Infrastructure.Settings.Authentication;
using CleanArch.Base.Template.Tests.Unit.Common.Fixtures;
using Microsoft.Extensions.Options;

namespace CleanArch.Base.Template.Tests.Unit.InfrastructureLayer.Authentication;

//public class JwtSettingsFixtureCollection : ICollectionFixture<JwtSettingsFixture>
//{ }

[CollectionDefinition(nameof(JwtSettingsFixture))]
public sealed class JwtSettingsFixture : BaseFixture, ICollectionFixture<JwtSettingsFixture>
{
    public IOptions<JwtSettings> GetJwtSettingsOptions()
    {
        return Options.Create(new JwtSettings
        {
            Secret = Faker.Random.String(128),
            Audience = Faker.Lorem.Text(),
            Issuer = Faker.Lorem.Text(),
            ExpiryMinutes = Faker.Random.Number(60),
        });
    }
}
