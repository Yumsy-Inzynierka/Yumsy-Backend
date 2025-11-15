using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.Profile.GetLikedPosts;

public class GetLikedPostsRequest
{
    public Guid UserId { get; set; }
    [FromQuery]
    public uint CurrentPage { get; set; }
}