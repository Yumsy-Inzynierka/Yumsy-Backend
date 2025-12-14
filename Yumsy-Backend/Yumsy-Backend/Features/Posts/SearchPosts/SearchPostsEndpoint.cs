using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.SearchPosts;

//[Authorize]
[ApiController]
[Route("api/posts")]
public class SearchPostsController : ControllerBase
{
    private readonly SearchPostsHandler _searchPostsHandler;
    private readonly IValidator<SearchPostsRequest> _validator;

    public SearchPostsController(SearchPostsHandler searchPostsHandler, IValidator<SearchPostsRequest> validator)
    {
        _searchPostsHandler = searchPostsHandler;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<SearchPostsResponse>> SearchPosts(
        [FromQuery] SearchPostsRequest searchPostsRequest,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(searchPostsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _searchPostsHandler.Handle(searchPostsRequest, cancellationToken);
            
        return Ok(response);
    }
}