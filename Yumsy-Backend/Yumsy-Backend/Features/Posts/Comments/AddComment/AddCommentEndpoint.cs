using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Comments.AddComment;

// [Authorized]
[ApiController]
[Route("api/posts/{postId}/comments")]
public class AddCommentEndpoint : ControllerBase
{
    private readonly AddCommentHandler _handler;
    private readonly IValidator<AddCommentRequest> _validator;
    
    public AddCommentEndpoint(AddCommentHandler handler, IValidator<AddCommentRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<ActionResult<AddCommentResponse>> AddComment(
        [FromRoute] Guid postId,
        [FromBody] AddCommentRequest addCommentRequest,
        CancellationToken cancellationToken
        )
    {
        addCommentRequest.PostId = postId;
        
        var validationResult = await _validator.ValidateAsync(addCommentRequest, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var userId = User.GetUserId();
        
        var response = await _handler.Handle(addCommentRequest, userId, cancellationToken);
        
        return Ok(response);
    }
}