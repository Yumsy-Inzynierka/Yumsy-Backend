using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;

[Authorize]
[ApiController]
[Route("api/shopping-lists")]
public class CreateShoppingListController : ControllerBase
{
    private readonly CreateShoppingListHandler _createShoppingListHandler;
    private readonly IValidator<CreateShoppingListRequest> _validator;
    
    public CreateShoppingListController(CreateShoppingListHandler createShoppingListHandler, IValidator<CreateShoppingListRequest> validator)
    {
        _createShoppingListHandler = createShoppingListHandler;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateShoppingListResponse>> Handle([FromRoute] CreateShoppingListRequest createShoppingListRequest, CancellationToken cancellationToken)
    {
        createShoppingListRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(createShoppingListRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _createShoppingListHandler.Handle(createShoppingListRequest, cancellationToken);
            
        return Created($"api/shoppingLists/{response.Id}", response);
    }
}