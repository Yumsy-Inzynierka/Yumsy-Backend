using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Users.Profile.GetLikedPosts;

[Authorize]
[ApiController]
[Route("api/profiles")]
public class Controller : ControllerBase
{
    private readonly GetLikedPostsHandler _getLikedPostsHandler;
    private readonly IValidator<GetLikedPostsRequest> _validator;

    public Controller(GetLikedPostsHandler getLikedPostsHandler, IValidator<GetLikedPostsRequest> validator)
    {
        _getLikedPostsHandler = getLikedPostsHandler;
        _validator = validator;
    }

    [HttpGet("liked-posts")]
    public async Task<ActionResult<GetLikedPostsResponse>> Handle(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        
        var response = await _getLikedPostsHandler.Handle(userId, cancellationToken);
            
        return Ok(response);
    }
}