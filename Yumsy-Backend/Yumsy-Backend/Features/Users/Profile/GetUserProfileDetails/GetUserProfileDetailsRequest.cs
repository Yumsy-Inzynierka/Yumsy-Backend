using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.Profile.GetUserProfileDetails;

public class GetUserProfileDetailsRequest
{
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }
}