using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

//[Authorize]
[ApiController]
[Route("api/shoppingLists")]
public class AddShoppingListController : ControllerBase
{
    private readonly AddShoppingListHandler _addShoppingListHandler;
    private readonly IValidator<AddShoppingListRequest> _validator;
    
    public AddShoppingListController(AddShoppingListHandler addShoppingListHandler, IValidator<AddShoppingListRequest> validator)
    {
        _addShoppingListHandler = addShoppingListHandler;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<ActionResult<AddShoppingListResponse>> Handle([FromBody] AddShoppingListRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var userId = User.GetUserId();
        
        var response = await _addShoppingListHandler.Handle(request, userId, cancellationToken);
            
        return Created($"api/shoppingLists/{response.Id}", response);
    }
}