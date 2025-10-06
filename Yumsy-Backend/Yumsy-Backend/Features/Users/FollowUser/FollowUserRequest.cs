using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.FollowUser;

public class FollowUserRequest
{
    public Guid FollowerId { get; set; }
    
    [FromBody]
    public FollowUserRequestBody Body { get; set; } = default!;
}

public class FollowUserRequestBody
{
    public Guid FollowingId { get; set; } 
}
