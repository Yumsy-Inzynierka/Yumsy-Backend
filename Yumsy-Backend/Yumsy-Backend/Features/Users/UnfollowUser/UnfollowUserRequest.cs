using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.UnfollowUser;

public class UnfollowUserRequest
{
    public Guid FollowerId { get; set; }
    
    [FromBody]
    public UnfollowUserRequestBody Body { get; set; } = default!;
}

public class UnfollowUserRequestBody
{
    public Guid FollowingId { get; set; } 
}