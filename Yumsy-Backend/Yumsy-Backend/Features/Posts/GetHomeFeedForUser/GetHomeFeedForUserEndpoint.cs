using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.GetHomeFeedForUser;

[Authorize]
[ApiController]
[Route("/api/posts")]
public class GetHomeFeedForUserEndpoint : ControllerBase
{
    private readonly GetHomeFeedForUserHandler _handler;
    private readonly IValidator<GetHomeFeedForUserRequest> _validator;

    public GetHomeFeedForUserEndpoint(GetHomeFeedForUserHandler handler, IValidator<GetHomeFeedForUserRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }

    [HttpGet("feed")]
    public async Task<ActionResult<GetHomeFeedForUserResponse>> GetHomeFeedPosts([FromRoute] GetHomeFeedForUserRequest getHomeFeedForUserRequest, CancellationToken cancellationToken)
    {
        getHomeFeedForUserRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(getHomeFeedForUserRequest, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var response =  await _handler.Handle(getHomeFeedForUserRequest, cancellationToken);
        
        return Ok(response);
    }
}