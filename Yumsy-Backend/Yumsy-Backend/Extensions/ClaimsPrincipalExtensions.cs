using System.Security.Claims;

namespace Yumsy_Backend.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            throw new UnauthorizedAccessException("Missing claim for user id");

        if (!Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException($"Invalid GUID in claim: {userIdClaim}");

        return userId;
    }
}