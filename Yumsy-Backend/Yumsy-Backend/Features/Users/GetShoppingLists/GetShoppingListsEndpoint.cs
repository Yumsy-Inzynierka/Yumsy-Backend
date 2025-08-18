using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.GetShoppingLists;

[Authorize]
[ApiController]
[Route("api/shoppingLists")]
public class GetShoppingListsEndpoint : ControllerBase
{
    private readonly GetShoppingListsHandler _getShoppingListsHandler;
    private readonly IValidator<GetShoppingListsRequest> _validator;
    
    public GetShoppingListsEndpoint(GetShoppingListsHandler getShoppingListsHandler, IValidator<GetShoppingListsRequest> validator)
    {
        _getShoppingListsHandler = getShoppingListsHandler;
        _validator = validator;
    }
    
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> Handle([FromRoute] GetShoppingListsRequest getShoppingListsRequest)
    {
        var validationResult = await _validator.ValidateAsync(getShoppingListsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        GetShoppingListsResponse getShoppingListsResponse = await _getShoppingListsHandler.Handle(getShoppingListsRequest);
            
        return Ok(getShoppingListsResponse);
    }
}