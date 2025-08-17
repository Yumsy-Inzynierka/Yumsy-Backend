namespace Yumsy_Backend.Features.Ingredients.SearchIngredient;

public class SearchIngredientsResponse
{
    public List<SearchIngredientResponse> Ingredients { get; set; }
    public int Offset { get; set; }
    public bool HasMore { get; set; }
}

public class SearchIngredientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal EnergyKcal100g { get; set; }
}