using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.ShoppingLists.GetShoppingLists;

[Authorize]
[ApiController]
[Route("api/shopping-lists")]
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
    public async Task<IActionResult> Handle([FromRoute] GetShoppingListsRequest getShoppingListsHandler)
    {
        getShoppingListsHandler.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(getShoppingListsHandler);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var response = await _getShoppingListsHandler.Handle(getShoppingListsHandler);
        return Ok(response);
    }
}