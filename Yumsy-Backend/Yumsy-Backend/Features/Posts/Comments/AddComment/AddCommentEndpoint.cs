using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.Comments.AddComment;

[Authorize]
[ApiController]
[Route("api/posts/")]
public class AddCommentEndpoint : ControllerBase
{
    private readonly AddCommentHandler _handler;
    private readonly IValidator<AddCommentRequest> _validator;
    
    public AddCommentEndpoint(AddCommentHandler handler, IValidator<AddCommentRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpPost("{postId:guid}/comments")]
    public async Task<ActionResult<AddCommentResponse>> AddComment([FromRoute] AddCommentRequest addCommentRequest, CancellationToken cancellationToken)
    {
        addCommentRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(addCommentRequest, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var response = await _handler.Handle(addCommentRequest, cancellationToken);
        
        return Ok(response);
    }
}