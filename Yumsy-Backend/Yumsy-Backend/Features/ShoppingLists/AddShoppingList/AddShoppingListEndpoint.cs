using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

[Authorize]
[ApiController]
[Route("api/shopping-lists")]
public class AddShoppingListController : ControllerBase
{
    private readonly AddShoppingListHandler _addShoppingListHandler;
    private readonly IValidator<AddShoppingListRequest> _validator;
    
    public AddShoppingListController(AddShoppingListHandler addShoppingListHandler, IValidator<AddShoppingListRequest> validator)
    {
        _addShoppingListHandler = addShoppingListHandler;
        _validator = validator;
    }
    
    [HttpPost("import")]
    public async Task<ActionResult<AddShoppingListResponse>> Handle([FromRoute] AddShoppingListRequest addShoppingListRequest, CancellationToken cancellationToken)
    {
        addShoppingListRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(addShoppingListRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _addShoppingListHandler.Handle(addShoppingListRequest, cancellationToken);
            
        return Created($"api/shoppingLists/{response.Id}", response);
    }
}