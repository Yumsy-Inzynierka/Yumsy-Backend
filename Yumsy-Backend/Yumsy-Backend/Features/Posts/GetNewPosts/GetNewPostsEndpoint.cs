using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.GetNewPosts;

//[Authorize]
[ApiController]
[Route("api/posts")]
public class GetNewPostsController : ControllerBase
{
    private readonly GetNewPostsHandler _getNewPostsHandler;
    private readonly IValidator<GetNewPostsRequest> _validator;

    public GetNewPostsController(GetNewPostsHandler getNewPostsHandler, IValidator<GetNewPostsRequest> validator)
    {
        _getNewPostsHandler = getNewPostsHandler;
        _validator = validator;
    }

    [HttpGet("new")]
    public async Task<ActionResult<GetNewPostsResponse>> Handle(
        [FromRoute] GetNewPostsRequest request,
        CancellationToken cancellationToken)
    {
        request.UserId = User.GetUserId();
        
        var response = await _getNewPostsHandler.Handle(request, cancellationToken);
            
        return Ok(response);
    }
}