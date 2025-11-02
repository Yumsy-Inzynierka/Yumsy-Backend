using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.Comments.GetPostComments;

[Authorize]
[ApiController]
[Route("api/posts")]
public class GetPostCommentsEndpoint : ControllerBase
{
    private readonly GetPostCommentsHandler _handler;
    private readonly IValidator<GetPostCommentsRequest> _validator;
    
    public GetPostCommentsEndpoint(GetPostCommentsHandler handler, IValidator<GetPostCommentsRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpGet("{postId:guid}/comments")]
    public async Task<ActionResult<GetPostCommentsResponse>> GetPostComments([FromRoute] GetPostCommentsRequest getPostCommentsRequest, CancellationToken cancellationToken)
    {
        getPostCommentsRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(getPostCommentsRequest, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var response = await _handler.Handle(getPostCommentsRequest, cancellationToken);
        
        return Ok(response);
    }
}