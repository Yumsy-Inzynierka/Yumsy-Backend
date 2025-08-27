using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Comments.AddComment;

// [Authorized]
[ApiController]
[Route("api/posts/{postId}/comments")]
public class AddCommentEndpoint : ControllerBase
{
    private readonly Handler _handler;
    private readonly IValidator<AddCommentRequest> _validator;
    
    public AddCommentEndpoint(Handler handler, IValidator<AddCommentRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddComment(
        [FromBody] AddCommentRequest addCommentRequest,
        CancellationToken cancellationToken
        )
    {
        return Ok();
    }
}