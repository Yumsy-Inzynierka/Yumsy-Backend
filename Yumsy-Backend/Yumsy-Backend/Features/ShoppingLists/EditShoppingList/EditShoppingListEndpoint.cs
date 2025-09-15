using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.ShoppingLists.EditShoppingList;

//[Authorize]
[ApiController]
[Route("api/shopping-lists")]
public class Controller : ControllerBase
{
    private readonly EditShoppingListHandler _editShoppingListHandler;
    private readonly IValidator<EditShoppingListRequest> _validator;

    public Controller(EditShoppingListHandler editShoppingListHandler, IValidator<EditShoppingListRequest> validator)
    {
        _editShoppingListHandler = editShoppingListHandler;
        _validator = validator;
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EditShoppingListResponse>> Handle(
        [FromRoute] Guid id,
        [FromBody] EditShoppingListRequest editShoppingListRequest,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(editShoppingListRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var userId = User.GetUserId();
        
        var response = await _editShoppingListHandler.Handle(id, editShoppingListRequest, userId, cancellationToken);
            
        return Ok(response);
    }
}