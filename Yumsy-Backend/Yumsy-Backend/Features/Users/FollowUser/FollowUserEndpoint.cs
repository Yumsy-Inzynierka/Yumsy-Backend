using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.FollowUser;

//[Authorize]
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
    
    [HttpPost("{followerId}/follow")]
    public async Task<IActionResult> Handle([FromRoute] Guid followerId, [FromBody] FollowUserRequest followUserRequest, CancellationToken cancellationToken)
    {
        var fullRequest = new FollowUserRequest
        {
            FollowingId = followUserRequest.FollowingId,
            FollowerId = followerId
        };
        
        var validationResult = await _validator.ValidateAsync(fullRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _followUserHandler.Handle(fullRequest, cancellationToken);
            
        return Created($"api/users/{response.FollowingId}/follow", response);
    }
}