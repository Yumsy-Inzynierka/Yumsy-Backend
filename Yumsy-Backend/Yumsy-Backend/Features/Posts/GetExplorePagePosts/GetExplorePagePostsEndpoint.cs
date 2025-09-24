using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;

//[Authorize]
[ApiController]
[Route("api/posts/explore-page")]
public class GetExplorePagePostsController : ControllerBase
{
    private readonly GetExplorePagePostsHandler _getExplorePagePostsHandler;
    private readonly IValidator<GetExplorePagePostsRequest> _validator;

    public GetExplorePagePostsController(GetExplorePagePostsHandler getExplorePagePostsHandler, IValidator<GetExplorePagePostsRequest> validator)
    {
        _getExplorePagePostsHandler = getExplorePagePostsHandler;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<GetExplorePagePostsResponse>> Handle([FromQuery] GetExplorePagePostsRequest getExplorePagePostsRequest, 
        CancellationToken cancellationToken)
    {
        // Igorze nie denerwuj się, jak frontend ogarnie autoryzacje to to usunę
        getExplorePagePostsRequest.UserId = new Guid("6e107bdf-63ea-4efe-a941-11f476055b20");
        
        //getExplorePagePostsRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(getExplorePagePostsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        
        var response = await _getExplorePagePostsHandler.Handle(getExplorePagePostsRequest, cancellationToken);
            
        return Ok(response);
    }
}