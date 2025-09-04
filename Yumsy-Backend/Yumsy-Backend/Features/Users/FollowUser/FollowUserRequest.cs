using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.FollowUser;

public class FollowUserRequest
{
    public Guid FollowerId { get; set; }
    
    [FromBody]
    public Guid FollowingId { get; set; }    
}
