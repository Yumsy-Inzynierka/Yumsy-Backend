using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Users.Profile.EditProfileDetails;

public class EditProfileDetailsHandler
{
    private readonly SupabaseDbContext _dbContext;

    public EditProfileDetailsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(EditProfileDetailsRequest addProfileDetailsRequest, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == addProfileDetailsRequest.UserId, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException($"User with ID: {addProfileDetailsRequest.UserId} not found.");
        
        var profileName = await _dbContext.Users
            .AnyAsync(u => u.ProfileName == addProfileDetailsRequest.Body.ProfileName, cancellationToken);
        
        if (profileName)
            throw new InvalidOperationException($"Profile name: {addProfileDetailsRequest.Body.ProfileName} already exists.");

        user.ProfileName = addProfileDetailsRequest.Body.ProfileName;
        user.ProfilePicture = addProfileDetailsRequest.Body.ProfilePicture;
        user.Bio = addProfileDetailsRequest.Body.Bio;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}