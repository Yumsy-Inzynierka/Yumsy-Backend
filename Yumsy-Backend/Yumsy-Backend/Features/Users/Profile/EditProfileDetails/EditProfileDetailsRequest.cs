using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.Profile.EditProfileDetails;

public class EditProfileDetailsRequest
{
    public Guid UserId { get; set; }

    [FromBody]
    public EditProfileDetailsRequestBody Body { get; set; } = default!;
}

public class EditProfileDetailsRequestBody
{
    public string? Username { get; set; }
    public string? ProfileName { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; } = string.Empty;
    public string? Bio { get; set; } = string.Empty;
}