using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.GetHomeFeed;

[ApiController]
[Route("/api/posts/feed")]
public class GetHomeFeedForUserEndpoint : ControllerBase
{
    private readonly GetHomeFeedForUserHandler _handler;
    private readonly GetHomeFeedForUserValidator _validator;

    public GetHomeFeedForUserEndpoint(GetHomeFeedForUserHandler handler, GetHomeFeedForUserValidator validator)
    {
        _handler = handler;
        _validator = validator;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<GetHomeFeedForUserResponse>> GetHomeFeedPosts(string userId, CancellationToken cancellationToken)
    {
        var request = new GetHomeFeedForUserRequest { UserId = userId };
        
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var response =  await _handler.Handle(request, cancellationToken);
        
        return Ok(response);
    }
}