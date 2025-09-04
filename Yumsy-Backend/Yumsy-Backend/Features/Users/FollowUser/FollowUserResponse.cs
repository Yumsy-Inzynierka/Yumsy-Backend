namespace Yumsy_Backend.Features.Users.FollowUser;

public record FollowUserResponse
{
    public Guid FollowingId { get; init; }
}
