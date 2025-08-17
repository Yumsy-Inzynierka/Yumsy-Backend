using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Ingredients.SearchIngredient;

[Authorize]
[ApiController]
[Route("/api/ingredients")]
public class SearchIngredientsEndpoint : ControllerBase
{
    private readonly SearchIngredientsHandler _handler;
    private readonly SearchIngredientValidator _validator;

    public SearchIngredientsEndpoint(SearchIngredientsHandler handler, SearchIngredientValidator validator)
    {
        _handler = handler;
        _validator = validator;
    }

    [HttpGet("search")]
    public async Task<ActionResult<SearchIngredientsResponse>> SearchIngredient(
        [FromQuery] SearchIngredientsRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var response = await _handler.Handle(request, cancellationToken);

        return Ok(response);
    }
}