using CleanArch.Base.Template.Infrastructure.Authentication;
using CleanArch.Base.Template.Infrastructure.Settings.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace CleanArch.Base.Template.Tests.Unit.InfrastructureLayer.Authentication;

[Collection(nameof(JwtSettingsFixture))]
public class JwtTokenGeneratorTests
{
    private readonly JwtSettingsFixture _fixture;
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly JwtTokenGenerator _sut;

    public JwtTokenGeneratorTests(JwtSettingsFixture fixture)
    {
        _fixture = fixture;
        _jwtSettings = _fixture.GetJwtSettingsOptions();
        _sut = new JwtTokenGenerator(_jwtSettings);
    }

    [Fact(DisplayName = "JwtTokenGenerator.GenerateToken() should return non-null")]
    [Trait("Infrastructure", "JwtTokenGenerator - GenerateToken")]
    public void GenerateToken_ShouldReturnNonNull()
    {
        // Act
        var token = _sut.GenerateToken();

        // Assert
        Assert.NotNull(token);
    }

    [Fact(DisplayName = "JwtTokenGenerator.GenerateToken() should return valid token")]
    [Trait("Infrastructure", "JwtTokenGenerator - GenerateToken")]
    public void GenerateToken_ShouldReturnValidToken()
    {
        // Act
        var token = _sut.GenerateToken();

        // Assert
        var tokenHandler = new JwtSecurityTokenHandler();
        var decodedToken = tokenHandler.ReadJwtToken(token);

        Assert.Equal(_jwtSettings.Value.Issuer, decodedToken.Issuer);
        Assert.Equal(_jwtSettings.Value.Audience, decodedToken.Audiences.First());
        Assert.NotEmpty(decodedToken.Claims);
    }

    [Fact(DisplayName = "JwtTokenGenerator.GenerateToken() should have valid expiry")]
    [Trait("Infrastructure", "JwtTokenGenerator - GenerateToken")]
    public void GenerateToken_ShouldHaveValidExpiry()
    {
        // Act
        var token = _sut.GenerateToken();
        var tokenHandler = new JwtSecurityTokenHandler();
        var decodedToken = tokenHandler.ReadJwtToken(token);

        // Assert
        var expiry = decodedToken.ValidTo - decodedToken.ValidFrom;
        Assert.Equal(_jwtSettings.Value.ExpiryMinutes, (int)expiry.TotalMinutes);
        Assert.True(expiry.TotalSeconds >= JwtSettings.Minimum_Expiry_Time);
    }
}
