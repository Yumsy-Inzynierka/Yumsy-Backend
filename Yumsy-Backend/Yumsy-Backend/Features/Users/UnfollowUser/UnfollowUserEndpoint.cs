using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.UnfollowUser;

//[Authorize]
[ApiController]
[Route("api/users")]
public class UnfollowUserEndpoint : ControllerBase
{
    private readonly UnfollowUserHandler _unfollowUserHandler;
    private readonly IValidator<UnfollowUserRequest> _validator;
    
    public UnfollowUserEndpoint(UnfollowUserHandler unfollowUserHandler, IValidator<UnfollowUserRequest> validator)
    {
        _unfollowUserHandler = unfollowUserHandler;
        _validator = validator;
    }
    
    [HttpDelete("{followerId}/unfollow")]
    public async Task<IActionResult> Handle([FromRoute] Guid followerId, [FromBody] UnfollowUserRequest followUserRequest, CancellationToken cancellationToken)
    {
        var fullRequest = new UnfollowUserRequest
        {
            FollowingId = followUserRequest.FollowingId,
            FollowerId = followerId
        };
        
        var validationResult = await _validator.ValidateAsync(fullRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _unfollowUserHandler.Handle(fullRequest, cancellationToken);
            
        return NoContent();
    }
}