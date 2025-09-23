using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.ShoppingLists.GetShoppingLists;

//[Authorize]
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
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> Handle([FromRoute] GetShoppingListsRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var response = await _getShoppingListsHandler.Handle(request);
        return Ok(response);
    }
}