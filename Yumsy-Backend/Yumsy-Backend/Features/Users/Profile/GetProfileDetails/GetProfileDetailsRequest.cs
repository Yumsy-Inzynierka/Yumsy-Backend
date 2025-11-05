using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.Profile.GetProfileDetails;

public class GetProfileDetailsRequest
{
    public Guid UserId { get; set; }
    
    [FromRoute(Name = "profileOwnerId")]
    public Guid ProfileOwnerId { get; set; }
}