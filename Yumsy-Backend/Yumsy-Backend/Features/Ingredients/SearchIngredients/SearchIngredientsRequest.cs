using System.ComponentModel.DataAnnotations;

namespace Yumsy_Backend.Features.Ingredients.SearchIngredient;

public class SearchIngredientsRequest
{
    public string Query { get; set; }

    public int Offset { get; set; } = 0;
}