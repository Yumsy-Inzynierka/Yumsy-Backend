using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.ShoppingLists.EditShoppingList;

[Authorize]
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

    [HttpPut("{shoppingListId:guid}")]
    public async Task<ActionResult<EditShoppingListResponse>> Handle([FromRoute] EditShoppingListRequest editShoppingListRequest, CancellationToken cancellationToken)
    {
        editShoppingListRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(editShoppingListRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _editShoppingListHandler.Handle(editShoppingListRequest, cancellationToken);
            
        return Ok(response);
    }
}