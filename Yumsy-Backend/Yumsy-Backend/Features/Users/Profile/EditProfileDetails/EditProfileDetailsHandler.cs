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
            .FirstOrDefaultAsync(u => u.Id == addProfileDetailsRequest.Id, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException($"User with ID: {addProfileDetailsRequest.Id} not found.");
        
        var profileName = await _dbContext.Users
            .AnyAsync(u => u.ProfileName == addProfileDetailsRequest.ProfileName, cancellationToken);
        
        if (profileName)
            throw new InvalidOperationException($"Profile name: {addProfileDetailsRequest.ProfileName} already exists.");

        user.ProfileName = addProfileDetailsRequest.ProfileName;
        user.ProfilePicture = addProfileDetailsRequest.ProfilePicture;
        user.Bio = addProfileDetailsRequest.Bio;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}