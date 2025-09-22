namespace Yumsy_Backend.Features.Users.Profile.EditProfileDetails;

public class EditProfileDetailsRequest
{
    public Guid Id { get; set; }
    public string ProfileName { get; set; }
    public string ProfilePicture { get; set; }
    public string Bio { get; set; }
}