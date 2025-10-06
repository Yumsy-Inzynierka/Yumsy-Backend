using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Users.UnfollowUser;

[Authorize]
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
    
    [HttpDelete("unfollow")]
    public async Task<IActionResult> Handle([FromRoute] UnfollowUserRequest followUserRequest, CancellationToken cancellationToken)
    {
        followUserRequest.FollowerId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(followUserRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _unfollowUserHandler.Handle(followUserRequest, cancellationToken);
            
        return NoContent();
    }
}