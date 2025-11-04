namespace Yumsy_Backend.Features.Ingredients.SearchIngredients;

public record SearchIngredientsResponse
{
    public List<SearchIngredientResponse> Ingredients { get; init; }
    public int Offset { get; init; }
    public bool HasMore { get; init; }
}

public record SearchIngredientResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal EnergyKcal100g { get; init; }
}