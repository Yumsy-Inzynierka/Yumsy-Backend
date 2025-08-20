using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;

//[Authorize]
[ApiController]
[Route("api/shoppingLists")]
public class DeleteShoppingListEndpoint : ControllerBase
{
    private readonly DeleteShoppingListHandler _detailsHandler;
    private readonly IValidator<DeleteShoppingListRequest> _validator;
    
    public DeleteShoppingListEndpoint(DeleteShoppingListHandler deleteShoppingListHandler, IValidator<DeleteShoppingListRequest> validator)
    {
        _detailsHandler = deleteShoppingListHandler;
        _validator = validator;
    }
    
    [HttpDelete("{shoppingListId}")]
    public async Task<IActionResult> Handle([FromRoute] DeleteShoppingListRequest deleteShoppingListRequest)
    {
        var validationResult = await _validator.ValidateAsync(deleteShoppingListRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _detailsHandler.Handle(deleteShoppingListRequest);
            
        return NoContent();
    }
}