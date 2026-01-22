using System.Security.Claims;
using FluentAssertions;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.UnitTests.Extensions;

public class ClaimsPrincipalExtensionsTests
{
    [Fact]
    public void GetUserId_Should_ReturnGuid_When_ClaimIsValid()
    {
        var userId = Guid.NewGuid();
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var result = principal.GetUserId();

        result.Should().Be(userId);
    }

    [Fact]
    public void GetUserId_Should_ThrowUnauthorizedAccessException_When_ClaimIsMissing()
    {
        var claims = Array.Empty<Claim>();
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var act = () => principal.GetUserId();

        act.Should().Throw<UnauthorizedAccessException>()
            .WithMessage("Missing claim for user id");
    }

    [Fact]
    public void GetUserId_Should_ThrowUnauthorizedAccessException_When_ClaimIsEmpty()
    {
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "") };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var act = () => principal.GetUserId();

        act.Should().Throw<UnauthorizedAccessException>()
            .WithMessage("Missing claim for user id");
    }

    [Fact]
    public void GetUserId_Should_ThrowUnauthorizedAccessException_When_ClaimIsInvalidGuid()
    {
        var invalidGuid = "not-a-valid-guid";
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, invalidGuid) };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var act = () => principal.GetUserId();

        act.Should().Throw<UnauthorizedAccessException>()
            .WithMessage($"Invalid GUID in claim: {invalidGuid}");
    }

    [Theory]
    [InlineData("abc123")]
    [InlineData("12345")]
    [InlineData("guid-like-but-invalid")]
    [InlineData("00000000-0000-0000-0000-00000000000g")]
    public void GetUserId_Should_ThrowUnauthorizedAccessException_ForInvalidGuidFormats(string invalidGuid)
    {
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, invalidGuid) };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var act = () => principal.GetUserId();

        act.Should().Throw<UnauthorizedAccessException>();
    }

    [Fact]
    public void GetUserId_Should_Work_WithDifferentGuidFormats()
    {
        var userId = Guid.NewGuid();

        var formats = new[]
        {
            userId.ToString("D"),
            userId.ToString("N"),
            userId.ToString("B"),
            userId.ToString("P")
        };

        foreach (var format in formats)
        {
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, format) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            var result = principal.GetUserId();

            result.Should().Be(userId);
        }
    }
}
