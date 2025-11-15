using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Users.Profile.GetLikedPosts;

[Authorize]
[ApiController]
[Route("api/profiles")]
public class GetLikedPostsController : ControllerBase
{
    private readonly GetLikedPostsHandler _getLikedPostsHandler;
    private readonly IValidator<GetLikedPostsRequest> _validator;

    public GetLikedPostsController(GetLikedPostsHandler getLikedPostsHandler, IValidator<GetLikedPostsRequest> validator)
    {
        _getLikedPostsHandler = getLikedPostsHandler;
        _validator = validator;
    }

    [HttpGet("liked-posts")]
    public async Task<ActionResult<GetLikedPostsResponse>> Handle(
        [FromRoute] GetLikedPostsRequest request,
        CancellationToken cancellationToken
        )
    {
        var userId = User.GetUserId();
        
        var response = await _getLikedPostsHandler.Handle(request, userId, cancellationToken);
            
        return Ok(response);
    }
}