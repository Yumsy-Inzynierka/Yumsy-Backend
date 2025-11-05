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

    public async Task Handle(EditProfileDetailsRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found.");

        if (!string.Equals(user.ProfileName, request.Body.ProfileName, StringComparison.OrdinalIgnoreCase))
        {
            var profileNameTaken = await _dbContext.Users
                .AnyAsync(u => u.ProfileName == request.Body.ProfileName && u.Id != request.UserId, cancellationToken);

            if (profileNameTaken)
                throw new InvalidOperationException($"Profile name '{request.Body.ProfileName}' is already taken.");
        }

        if (!string.Equals(user.Username, request.Body.Username, StringComparison.OrdinalIgnoreCase))
        {
            var usernameTaken = await _dbContext.Users
                .AnyAsync(u => u.Username == request.Body.Username && u.Id != request.UserId, cancellationToken);

            if (usernameTaken)
                throw new InvalidOperationException($"Username '{request.Body.Username}' is already taken.");
        }

        var hasChanges =
            user.Username != request.Body.Username ||
            user.ProfileName != request.Body.ProfileName ||
            user.ProfilePicture != request.Body.ProfilePicture ||
            user.Bio != request.Body.Bio;

        if (hasChanges)
        {
            user.Username = request.Body.Username;
            user.ProfileName = request.Body.ProfileName;
            user.ProfilePicture = request.Body.ProfilePicture;
            user.Bio = request.Body.Bio;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
