using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;

//[Authorize]
[ApiController]
[Route("api/shopping-lists")]
public class AddShoppingListController : ControllerBase
{
    private readonly CreateShoppingListHandler _createShoppingListHandler;
    private readonly IValidator<CreateShoppingListRequest> _validator;
    
    public AddShoppingListController(CreateShoppingListHandler createShoppingListHandler, IValidator<CreateShoppingListRequest> validator)
    {
        _createShoppingListHandler = createShoppingListHandler;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateShoppingListResponse>> Handle([FromBody] CreateShoppingListRequest request, CancellationToken cancellationToken)
    {
        //request.UserId = User.GetUserId();
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _createShoppingListHandler.Handle(request, cancellationToken);
            
        return Created($"api/shoppingLists/{response.Id}", response);
    }
}