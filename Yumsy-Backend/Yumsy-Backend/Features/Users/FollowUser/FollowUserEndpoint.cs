using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Users.FollowUser;

[Authorize]
[ApiController]
[Route("api/users")]
public class FollowUserEndpoint : ControllerBase
{
    private readonly FollowUserHandler _followUserHandler;
    private readonly IValidator<FollowUserRequest> _validator;
    
    public FollowUserEndpoint(FollowUserHandler followUserHandler, IValidator<FollowUserRequest> validator)
    {
        _followUserHandler = followUserHandler;
        _validator = validator;
    }
    
    [HttpPost("follow")]
    public async Task<IActionResult> Handle([FromRoute] FollowUserRequest followUserRequest, CancellationToken cancellationToken)
    {
        followUserRequest.FollowerId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(followUserRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _followUserHandler.Handle(followUserRequest, cancellationToken);
            
        return Created($"api/users/follow", response);
    }
}