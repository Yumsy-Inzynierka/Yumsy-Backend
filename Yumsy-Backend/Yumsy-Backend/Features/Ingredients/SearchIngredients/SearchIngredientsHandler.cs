using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Ingredients.SearchIngredient;

public class SearchIngredientsHandler
{
    private readonly SupabaseDbContext _context;

    public SearchIngredientsHandler(SupabaseDbContext context)
    {
        _context = context;
    }

    public async Task<SearchIngredientsResponse> Handle(SearchIngredientsRequest request,
        CancellationToken cancellationToken)
    {
        const int pageSize = 20;
        
        var ingredients = await _context.Ingredients
            .FromSqlRaw("SELECT * FROM search_ingredients({0})", request.Query)
            .Skip(request.Offset)
            .Take(pageSize + 1)
            .Select(i => new SearchIngredientResponse
            {
                Id = i.Id,
                Name = i.Name,
                EnergyKcal100g = i.EnergyKcal100g
            })
            .ToListAsync(cancellationToken);

        // logika sprawdzania czy są jeszcze jakieś składniki dla tego query
        var hasMore = ingredients.Count > pageSize; 
        if (hasMore)
            ingredients = ingredients.Take(pageSize).ToList();

        return new SearchIngredientsResponse()
        {
            Ingredients = ingredients,
            Offset = request.Offset,
            HasMore = hasMore
        };
    }
}