using System.Security.Claims;

namespace Yumsy_Backend.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userIdClaim = principal.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            throw new UnauthorizedAccessException("Missing claim for user id");
        
        return Guid.Parse(userIdClaim);
    }
}