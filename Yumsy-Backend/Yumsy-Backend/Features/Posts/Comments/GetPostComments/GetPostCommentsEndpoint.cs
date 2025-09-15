using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Comments.GetPostComments;

// [Authorized]
[ApiController]
[Route("api/posts/{postId}/comments")]
public class GetPostCommentsEndpoint : ControllerBase
{
    private readonly GetPostCommentsHandler _handler;
    private readonly IValidator<GetPostCommentsRequest> _validator;
    
    public GetPostCommentsEndpoint(GetPostCommentsHandler handler, IValidator<GetPostCommentsRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpGet]
    public async Task<ActionResult<GetPostCommentsResponse>> GetPostComments(
        [FromRoute] GetPostCommentsRequest request,
        CancellationToken cancellationToken
        )
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var response = await _handler.Handle(request, cancellationToken);
        
        return Ok(response);
    }
}