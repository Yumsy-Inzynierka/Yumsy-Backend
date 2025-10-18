using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.Profile.GetProfileDetails;

public class GetProfileDetailsRequest
{
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }
}