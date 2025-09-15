namespace Yumsy_Backend.Features.Users.Profile.CreateProfile;

public class AddProfileDetailsRequest
{
    public Guid Id { get; set; }
    public string ProfileName { get; set; }
    public string ProfilePicture { get; set; }
    public string Bio { get; set; }
}