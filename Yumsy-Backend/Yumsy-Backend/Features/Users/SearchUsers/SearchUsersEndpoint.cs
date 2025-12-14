using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Users.SearchUsers;

//[Authorize]
[ApiController]
[Route("api/users")]
public class SearchUsersController : ControllerBase
{
    private readonly SearchUsersHandler _searchUsersHandler;
    private readonly IValidator<SearchUsersRequest> _validator;

    public SearchUsersController(SearchUsersHandler searchUsersHandler, IValidator<SearchUsersRequest> validator)
    {
        _searchUsersHandler = searchUsersHandler;
        _validator = validator;
    }

    [HttpGet("search")]
    public async Task<ActionResult<SearchUsersResponse>> Handle([FromQuery] SearchUsersRequest searchUsersRequest,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(searchUsersRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _searchUsersHandler.Handle(searchUsersRequest, cancellationToken);
            
        return Ok(response);
    }
}